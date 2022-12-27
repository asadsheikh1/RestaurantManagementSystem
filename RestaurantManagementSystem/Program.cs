using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Secret Key: ");
            string secretKey = Console.ReadLine();

            ProxyLogin proxyLogin = new ProxyLogin(userName, password, secretKey);
            bool isTrue = proxyLogin.performLogin();

            if (isTrue)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("\t LOGIN SUCCESSFULL");

                Console.WriteLine("========================================");
                Console.WriteLine("\t LIST OF EMPLOYEES");
                Console.WriteLine("========================================");

                Employee restaurantManager = new Employee();
                restaurantManager.name = "Restaurant Manager";

                Employee shiftManager1 = new Employee();
                shiftManager1.name = "Shift Manager # 1";

                Employee shiftManager2 = new Employee();
                shiftManager2.name = "Shift Manager # 2";

                Employee chef1 = new Employee();
                chef1.name = "Chinese Chef";

                Employee chef2 = new Employee();
                chef2.name = "Italian Chef";

                Employee waiterHead = new Employee();
                waiterHead.name = "Waiter Head";

                Employee valetHead = new Employee();
                valetHead.name = "Valet Head";

                Employee waiter1 = new Employee();
                waiter1.name = "Morning Waiter";

                Employee waiter2 = new Employee();
                waiter2.name = "Night Waiter";

                Employee valet1 = new Employee();
                valet1.name = "Valet # 1";

                Employee valet2 = new Employee();
                valet2.name = "Valet # 2";

                restaurantManager.subOrdinates.Add(shiftManager1);
                restaurantManager.subOrdinates.Add(shiftManager2);
                shiftManager1.subOrdinates.Add(chef1);
                shiftManager1.subOrdinates.Add(chef2);
                shiftManager2.subOrdinates.Add(waiterHead);
                shiftManager2.subOrdinates.Add(valetHead);
                waiterHead.subOrdinates.Add(waiter1);
                waiterHead.subOrdinates.Add(waiter2);
                valetHead.subOrdinates.Add(valet1);
                valetHead.subOrdinates.Add(valet2);

                Employee.record(restaurantManager);

                Console.WriteLine("========================================");
                Console.WriteLine("\t\t VENDORS");
                Console.WriteLine("========================================");
                VendorFactory vf = new VendorFactory();

                Vendor v1 = vf.getVendor("Manufacturers");
                v1.get();
                Vendor v2 = vf.getVendor("Retailers");
                v2.get();
                Vendor v3 = vf.getVendor("Wholesalers");
                v3.get();
                Vendor v4 = vf.getVendor("Service");
                v4.get();

                Console.WriteLine("========================================");
                Console.WriteLine("\t\t DEALS");
                Console.WriteLine("========================================");
                MealBuilder mealBuilder = new MealBuilder();

                Console.WriteLine("DEAL # 1");
                Meal meal1 = mealBuilder.Deal1();
                meal1.print();
                Console.WriteLine();
                Console.WriteLine("DEAL # 2");
                Meal meal2 = mealBuilder.Deal2();
                meal2.print();


                Console.WriteLine("========================================");
                Console.WriteLine("\t\t PAY");
                Console.WriteLine("========================================");

                Discounter s = new Discounter();

                s.setDiscount(new Cash());
                s.operate(1200);
                Console.WriteLine();

                s.setDiscount(new Card());
                s.operate(1000);
            }
            else
            {
                Console.WriteLine("INVALID LOGIN");
            }
        }
        public interface ILogin
        {
            bool performLogin();
        }
        public class Login : ILogin
        {

            public bool performLogin()
            {
                return true;
            }
        }
        public class ProxyLogin : ILogin
        {
            private ILogin iLogin;
            private string userName;
            private string password;
            private string secretKey;
            public ProxyLogin(string userName, string password, string secretKey)
            {
                this.userName = userName;
                this.password = password;
                this.secretKey = secretKey;
            }
            public bool performLogin()
            {
                if (userName == "Asad" && password == "123" && secretKey == "786")
                {
                    iLogin = new Login();
                    iLogin.performLogin();
                    Console.WriteLine("Login Successful");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        class VendorFactory
        {
            public Vendor getVendor(string name)
            {
                if (name == "Manufacturers")
                {
                    return new Manufacturers();
                }
                else if (name == "Retailers")
                {
                    return new Retailers();
                }
                else if (name == "Wholesalers")
                {
                    return new Wholesalers();
                }
                else
                {
                    return new Service();
                }
            }
        }
        public interface Vendor
        {
            void get();
        }
        public class Manufacturers : Vendor
        {
            public void get()
            {
                Console.WriteLine("1 - Manufacturer");
            }
        }
        public class Retailers : Vendor
        {
            public void get()
            {
                Console.WriteLine("2 - Retailers");
            }
        }
        public class Wholesalers : Vendor
        {
            public void get()
            {
                Console.WriteLine("3 - Wholesalers");
            }
        }
        public class Service : Vendor
        {
            public void get()
            {
                Console.WriteLine("4 - Service");
            }
        }
        public interface Food
        {
            void getName();
            int getPrice();
        }
        public class VegBurger : Food
        {
            public void getName()
            {
                Console.WriteLine("This is Veg Burger");
            }
            public int getPrice()
            {
                return 100;
            }
        }
        public class ChickenBurger : Food
        {
            public void getName()
            {
                Console.WriteLine("This is Chicken Burger");
            }
            public int getPrice()
            {
                return 200;
            }
        }
        public class Coke : Food
        {
            public void getName()
            {
                Console.WriteLine("This is Coke");
            }
            public int getPrice()
            {
                return 40;
            }
        }
        public class Pepsi : Food
        {
            public void getName()
            {
                Console.WriteLine("This is Pepsi");
            }
            public int getPrice()
            {
                return 50;
            }
        }
        class Employee
        {
            public string name;
            public List<Employee> subOrdinates = new List<Employee>();

            public static void record(Employee n)
            {
                if (n.subOrdinates.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (Employee item in n.subOrdinates)
                    {
                        Console.WriteLine(item.name);
                        Console.WriteLine("---");
                        record(item);
                        Console.WriteLine("end");
                    }
                }
            }
        }
        public class Discounter
        {
            private Discount discount;

            public void setDiscount(Discount d)
            {
                discount = d;
            }
            public void operate(int num)
            {
                discount.getDiscount(num);
            }
        }
        public interface Discount
        {
            void getDiscount(int num);
        }
        public class Cash : Discount
        {
            public void getDiscount(int num)
            {
                Console.WriteLine("Pay by Cash");
                Console.WriteLine(num);
            }
        }
        public class Card : Discount
        {
            public void getDiscount(int num)
            {
                Console.WriteLine("Pay by Card - 20% discount");
                Console.WriteLine(num * 0.8);
            }
        }

        class FoodFactory
        {
            public Food getFood(string name)
            {
                if (name == "Chicken Burger")
                {
                    return new ChickenBurger();
                }
                else if (name == "Veg Burger")
                {
                    return new VegBurger();
                }
                else if (name == "Coke")
                {
                    return new Coke();
                }
                else
                {
                    return new Pepsi();
                }
            }
        }
        public class MealBuilder
        {
            public Meal Deal1()
            {
                Meal m = new Meal();
                m.addItem(new Coke());
                m.addItem(new ChickenBurger());
                return m;
            }
            public Meal Deal2()
            {
                Meal m = new Meal();
                m.addItem(new Pepsi());
                m.addItem(new VegBurger());
                return m;
            }
        }
        public class Meal
        {
            public List<Food> items = new List<Food>();

            public void addItem(Food i)
            {
                items.Add(i);
            }
            public void print()
            {
                foreach (Food item in items)
                {
                    item.getName();
                }
            }
        }
        public interface Item
        {
            string getName();
            Packing getPack();
            int getPrice();
        }
        public interface Packing
        {
            string getPackingType();
        }
        public abstract class Burger : Item
        {
            public abstract string getName();
            public abstract int getPrice();
            public Packing getPack()
            {
                return new Wrapper();
            }
        }
        public abstract class ColdDrink : Item
        {
            public abstract string getName();
            public abstract int getPrice();
            public Packing getPack()
            {
                return new Bottle();
            }
        }

        public class Wrapper : Packing
        {
            public string getPackingType()
            {
                return "Wrapper";
            }
        }
        public class Bottle : Packing
        {
            public string getPackingType()
            {
                return "Bottle";
            }
        }
    }
}