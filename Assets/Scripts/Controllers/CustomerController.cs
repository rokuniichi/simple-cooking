using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerController : BaseController<CustomerController>
{
    public GameObject    PoolRoot;
    public OrderSetup    OrderSetup;
    public CustomerSetup CustomerSetup;
    public List<Place>   CustomerPlaces;

    public int CustomersRemaining { get; private set; }
    public int OrdersRemaining    { get; private set; }
    
    public int CustomersTotal    => _config.CustomersTotal;
    public int OrdersTotal       => _config.OrdersTotal;
    public int OrdersPerCustomer => _config.OrdersPerCustomer;
    
    public event Action CustomersRemainingChanged;

    GameController _gc;
    
    MainConfig _config;

    OrderHolder    _orderHolder;
    CustomerHolder _customerHolder;

    List<Customer> _customers;

    int _ordersServed;
    int _customerIndex;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (CustomersRemaining > 0) EngageCustomer();
        if (_ordersServed == OrdersTotal) _gc.Stop();
    }
    
    public void TryServeOrder(string order)
    {
        foreach (var customer in _customers)
        {
            if (customer.TryServeOrder(order))
            {
                _ordersServed++;
                return;
            }
        }
    }

    void Init()
    {
        _gc = GameController.Instance;
        _config = _gc.Config;
        CustomersRemaining = CustomersTotal;
        OrdersRemaining    = OrdersTotal;
        if (OrdersTotal < CustomersTotal)
        {
            Debug.LogError("Number of orders is less then number of customers!");
        }

        _customerHolder = PoolRoot.AddComponent<CustomerHolder>();
        _orderHolder = PoolRoot.AddComponent<OrderHolder>();
        _customerHolder.PopulateHolder(CustomerSetup.CustomerEntries, CustomersTotal);
        _orderHolder.PopulateHolder(OrderSetup.OrderEntries, OrdersTotal);

        foreach (var customerPlace in CustomerPlaces)
        {
            customerPlace.Free();
        }

        _customers = new List<Customer>();
    }

    void EngageCustomer()
    {
        var place = CustomerPlaces.Find(x => x.IsFree);
        if (place)
        {
            var customer = _customerHolder.GetObject();
            if (customer)
            {
                customer.Place(place);
                var n = Random.Range(1, Mathf.Clamp((OrdersRemaining - CustomersRemaining) + 1, 1, OrdersPerCustomer) + 1);
                for (var i = n; i > 0; i--)
                {
                    var order = _orderHolder.GetObject();
                    customer.CreateOrder(order);
                }
                _customers.Add(customer);
                OrdersRemaining -= n;
                CustomersRemaining--;
                CustomersRemainingChanged?.Invoke();
            }
        }
    }
}