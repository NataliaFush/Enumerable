using System.Collections;

namespace EnumerableTest
{
    class Week : IEnumerable<string>
    {
        string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                         "Friday", "Saturday", "Sunday" };
        IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)days).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => days.GetEnumerator();
    }

    class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Customer : IEnumerable<Order>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer(IEnumerable<Order> orders)
        {
            _orders = orders.ToList();
        }
        private IEnumerable<Order> _orders;

        public IEnumerator<Order> GetEnumerator()
        {
            return new OrderEnumerator<Order>(_orders);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new OrderEnumerator<Order>(_orders);
        }
    }

    class OrderEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        private readonly IEnumerable<T> _list;
        private int _index;
        private T _current;

        private int GetCount()
        {
            int count = 0;
            foreach (var item in _list)
            {
                count++;
            }
            return count;
        }

        private T GetCurrent(int index)
        {
            int count = 0;
            foreach (T item in _list)
            {
                if (count++ == index)
                {
                    return item;
                }
            }
            return default(T);
        }
        public OrderEnumerator(IEnumerable<T> orders)
        {
            _list = orders;
            _index = -1;
            _current = default;
        }

        public T Current
        {
            get
            {
                return _current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (_index < GetCount() - 1)
            {
                _index++;
                _current = GetCurrent(_index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            _index = -1;
            _current = default;
        }
    }



    class Run
    {
        static void Main(string[] args)
        {
            var customer = new Customer(new List<Order>()
            {
                new Order() { Id = 1, Name = "Order One"},
                new Order() { Id = 2, Name = "Order Second"},
                new Order() { Id = 3, Name = "Order Three"},
                new Order() { Id = 4, Name = "Order Four"}
            });

            foreach (var item in customer)
            {
                Console.WriteLine(item.Name);
            }

      

            //var enumerator = customer.GetEnumerator();
            //try
            //{
            //	while (enumerator.MoveNext())
            //	{
            //		var current = enumerator.Current;

            //		{
            //			Console.WriteLine(current.Name);
            //		}
            //	}
            //}
            //catch (Exception)
            //{
            //	throw;
            //}
            //finally
            //{
            //	enumerator.Reset();
            //	enumerator.Dispose();
            //}





        }
    }
}