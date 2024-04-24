using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_desktop
{
    public class Statisztika
    {
        public static List<Car> cars = new List<Car>();

        public static void Beolvasas()
        {
            cars.Clear();
            try
            {
                using var conn = new MySqlConnection("Server=localhost;Database=probavizsga;Uid=root;Pwd=;");
                conn.Open();

                var comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM cars";
                var results = comm.ExecuteReader();

                while (results.Read())
                {
                    int id = results.GetInt32("id");
                    string plate = results.GetString("license_plate_number");
                    string brand = results.GetString("brand");
                    string model = results.GetString("model");
                    int daily_cost = results.GetInt32("daily_cost");
                    cars.Add(new Car(id, plate, brand, model, daily_cost));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error" + ex.Message);
            }
        }

        public static int Olcsok()
        {
            int db = 0;
            foreach (var car in cars)
            {
                if (car.Daily_cost < 20000)
                {
                    db++;
                }
            }

            return db;
        }

        public static string Van()
        {
            bool van = false;
            foreach (var car in cars)
            {
                if (car.Daily_cost > 26000)
                {
                    van = true;
                }
            }

            if (van)
            {
                return "Van";
            }
            else
            {
                return "Nincs";
            }
        }

        public static string Draga()
        {
            int max = int.MinValue;
            Car resultCar = null;

            foreach (var car in cars)
            {
                if (car.Daily_cost > max)
                {
                    max = car.Daily_cost;
                    resultCar = car;
                }
            }

            return $"{resultCar.License_plate_number} - {resultCar.Brand} - {resultCar.Model} - {resultCar.Daily_cost} Ft";
        }

        public static void Markak()
        {
            Dictionary<string,int> brands = new Dictionary<string,int>();

            foreach (var car in cars)
            {
                if(!brands.ContainsKey(car.Brand))
                {
                    brands.Add(car.Brand, 0);
                }
            }

            foreach (var car in cars)
            {
                brands[car.Brand]++;
            }

            foreach (var brand in brands)
            {
                Console.WriteLine("\t"+brand.Key+": "+brand.Value);
            }
        }

        public static string Car(string plate)
        {
            Car resultCar = null;
            foreach (var car in cars)
            {
                if (car.License_plate_number == plate)
                {
                    resultCar = car;
                }
            }

            if (resultCar == null)
            {
                return "Nincs ilyen autó";
            }
            else if (resultCar.Daily_cost > 25000)
            {
                return "A megadott autó napidíja nagyobb, mint 25.000 Ft";
            }
            else
            {
                return "A megadott autó napidíja nem nagyobb, mint 25.000 Ft";
            }
        }
    }
}
