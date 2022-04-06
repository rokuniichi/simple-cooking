using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerController : BaseController<CustomerController>
{
    public GameObject      PoolRoot;
    public OrderSetup      OrderSetup;
    public CustomerSetup   CustomerSetup;
    public List<Place>     CustomerPlaces;
    public Transform       CustomerBackground;
    public List<Transform> CustomerWaypoints;

    public int CustomersRemaining { get; private set; }
    public int OrdersRemaining { get; private set; }

    public int CustomersTotal    => _config.CustomersTotal;
    public int OrdersTotal       => _config.OrdersTotal;
    public int OrdersPerCustomer => _config.OrdersPerCustomer;

    public event Action CustomersRemainingChanged;

    GameController _gc;
    MainConfig     _config;
    OrderHolder    _orderHolder;
    CustomerHolder _customerHolder;

    List<Customer> _customers = new List<Customer>();

    int   _ordersServed;
    float _waitTime;

    void Update()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f && CustomersRemaining > 0)
        {
            EngageCustomer();
        }
    }

    protected override void PreInit()
    {
        _customerHolder = PoolRoot.AddComponent<CustomerHolder>();
        _orderHolder = PoolRoot.AddComponent<OrderHolder>();
    }

    public void Init()
    {
        _gc = GameController.Instance;
        _config = _gc.Config;
        CustomersRemaining = CustomersTotal;
        OrdersRemaining = OrdersTotal;
        if (OrdersTotal < CustomersTotal)
        {
            Debug.LogError("Number of orders is less then number of customers!");
        }

        _customerHolder.PopulateHolder(CustomerSetup.CustomerEntries, CustomersTotal);
        _orderHolder.PopulateHolder(OrderSetup.OrderEntries, OrdersTotal);
        _ordersServed = 0;
        _waitTime     = 1f;

        foreach (var customer in _customers)
        {
            customer.AnimateDeparture(CustomerBackground, GetCustomerWaypoint());
        }

        _customers.Clear();

        foreach (var customerPlace in CustomerPlaces)
        {
            customerPlace.Free();
        }
    }

    public void TryServeOrder(string order)
    {
        foreach (var customer in _customers)
        {
            if (customer.TryServeOrder(order))
            {
                _ordersServed++;
                if (!customer.IsServable())
                {
                    customer.AnimateDeparture(CustomerBackground, GetCustomerWaypoint());
                    _customers.Remove(customer);
                    _waitTime = 1f;
                }

                CheckWinCondition();
                return;
            }
        }
    }

    void EngageCustomer()
    {
        var place = CustomerPlaces.Find(x => x.IsFree);
        if (place)
        {
            var customer = _customerHolder.GetObject();
            if (customer)
            {
                var orders = new List<Order>();
                var n = Random.Range(1, Mathf.Clamp((OrdersRemaining - CustomersRemaining) + 1, 1, OrdersPerCustomer) + 1);
                for (var i = n; i > 0; i--)
                {
                    orders.Add(_orderHolder.GetObject());
                }

                customer.AnimateArrival(CustomerBackground, GetCustomerWaypoint(), place, orders);
                _customers.Add(customer);
                OrdersRemaining -= n;
                CustomersRemaining--;
                CustomersRemainingChanged?.Invoke();
                _waitTime = 1f;
            }
        }
    }

    Transform GetCustomerWaypoint()
    {
        return CustomerWaypoints[Random.Range(0, CustomerWaypoints.Count)];
    }

    void CheckWinCondition()
    {
        if (_ordersServed == OrdersTotal || _customers.Count == 0) _gc.Win();
    }
}