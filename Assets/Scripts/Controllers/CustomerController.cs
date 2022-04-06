using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerController : BaseController<CustomerController>
{
    public GameObject          PoolRoot;
    public OrderSetup          OrderSetup;
    public CustomerSetup       CustomerSetup;
    public List<CustomerPlace> CustomerPlaces;

    public int CustomersRemaining { get; private set; }
    public int OrdersRemaining    { get; private set; }
    
    public int CustomersTotal    => _config.CustomersTotal;
    public int OrdersTotal       => _config.OrdersTotal;
    public int OrdersPerCustomer => _config.OrdersPerCustomer;
    
    public event Action CustomersRemainingChanged;

    MainConfig _config;

    OrderPool    _orders;
    CustomerPool _customers;

    void Start()
    {
        Init();
    }

    void Update()
    {
        EngageCustomers();
    }

    void Init()
    {
        _config = GameController.Instance.Config;
        CustomersRemaining = CustomersTotal;
        OrdersRemaining    = OrdersTotal;
        if (OrdersTotal < CustomersTotal)
        {
            Debug.LogError("Number of orders is less then number of customers!");
        }

        _customers = PoolRoot.AddComponent<CustomerPool>();
        _orders = PoolRoot.AddComponent<OrderPool>();
        _customers.PopulatePool(CustomerSetup.CustomerEntries, CustomersTotal);
        _orders.PopulatePool(OrderSetup.OrderEntries, OrdersTotal);
    }

    void EngageCustomers()
    {
        var place = CustomerPlaces.Find(x => x.IsFree);
        if (place)
        {
            var customer = _customers.GetPooledObject();
            if (customer)
            {
                place.Engage(customer);
                var n = Random.Range(1, Mathf.Clamp((OrdersRemaining - CustomersRemaining) + 1, 1, OrdersPerCustomer) + 1);
                for (var i = n; i > 0; i--)
                {
                    var order = _orders.GetPooledObject();
                    customer.CreateOrder(order);
                }
                OrdersRemaining -= n;
                CustomersRemaining--;
                CustomersRemainingChanged?.Invoke();
            }
        }
    }
}