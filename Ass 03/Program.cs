using System;
using System.Globalization;

namespace CompanyEmployeeApp
{
    public enum Gender
    {
        M, // Male
        F  // Female
    }

    public enum SecurityLevel
    {
        Guest,
        Developer,
        Secretary,
        DBA,
        SecurityOfficer // Full Permissions
    }

    // Class to represent Hiring Date
    public class HiringDate : IComparable<HiringDate>
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public HiringDate(int day, int month, int year)
        {
            if (!IsValidDate(day, month, year))
                throw new ArgumentException("Invalid date provided.");

            Day = day;
            Month = month;
            Year = year;
        }

        private bool IsValidDate(int day, int month, int year)
        {
            try
            {
                DateTime dt = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CompareTo(HiringDate other)
        {
            DateTime thisDate = new DateTime(Year, Month, Day);
            DateTime otherDate = new DateTime(other.Year, other.Month, other.Day);
            return thisDate.CompareTo(otherDate);
        }

        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
    }

    // Class for Employee
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SecurityLevel Security { get; set; }
        public decimal Salary { get; set; }
        public HiringDate HireDate { get; set; }
        public Gender Gender { get; set; }

        public Employee(int id, string name, SecurityLevel security, decimal salary, HiringDate hireDate, Gender gender)
        {
            ID = id;
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be empty.") : name;
            Security = security;
            Salary = salary >= 0 ? salary : throw new ArgumentException("Salary must be non-negative.");
            HireDate = hireDate ?? throw new ArgumentNullException(nameof(hireDate));
            Gender = gender;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Security: {Security}, Gender: {Gender}, " +
                   $"Salary: {string.Format(CultureInfo.InvariantCulture, "{0:C}", Salary)}, Hire Date: {HireDate}";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("========== Company Employee System ==========\n");

            Employee[] EmpArr = new Employee[3];

            EmpArr[0] = new Employee(101, "Ahmed", SecurityLevel.DBA, 20000m, new HiringDate(10, 3, 2020), Gender.M);
            EmpArr[1] = new Employee(102, "Fatma", SecurityLevel.Guest, 8000m, new HiringDate(15, 7, 2022), Gender.F);
            EmpArr[2] = new Employee(103, "Khaled", SecurityLevel.SecurityOfficer, 30000m, new HiringDate(5, 1, 2018), Gender.M);

            Console.WriteLine(">>> All Employees:");
            foreach (var emp in EmpArr)
                Console.WriteLine(emp);

            Array.Sort(EmpArr, (e1, e2) => e1.HireDate.CompareTo(e2.HireDate));

            Console.WriteLine("\n>>> Employees Sorted by Hire Date:");
            foreach (var emp in EmpArr)
                Console.WriteLine(emp);

            int boxingCount = 0;
            object box;
            foreach (var emp in EmpArr)
            {
                box = emp.HireDate.Day;    
                int day = (int)box;        
                boxingCount += 2;
            }

            Console.WriteLine($"\n>>> Estimated Boxing/Unboxing Operations: {boxingCount}");
        }
    }
}
