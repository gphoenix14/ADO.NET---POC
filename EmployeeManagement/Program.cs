using System;
using EmployeeManagement.Models;


namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {

            EmployeeDataAccessLayer employeeDAL = new EmployeeDataAccessLayer();
            while (true)
            {
                Console.WriteLine("Seleziona un'opzione:");
                Console.WriteLine("1. Crea un utente");
                Console.WriteLine("2. Visualizza dati utente (inserendo un ID)");
                Console.WriteLine("3. Aggiorna un utente (inserendo l'ID)");
                Console.WriteLine("4. Lista tutti gli utenti");
                Console.WriteLine("5. Elimina un utente (fornendo il suo ID)");
                Console.WriteLine("6. Esci");

                int scelta = Convert.ToInt32(Console.ReadLine());

                switch (scelta)
                {
                    case 1:
                        CreaUtente(employeeDAL);
                        break;
                    case 2:
                        VisualizzaUtente(employeeDAL);
                        break;
                    case 3:
                        AggiornaUtente(employeeDAL);
                        break;
                    case 4:
                        ListaTuttiGliUtenti(employeeDAL);
                        break;
                    case 5:
                        EliminaUtente(employeeDAL);
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }
            }
        }

        static void CreaUtente(EmployeeDataAccessLayer employeeDAL)
        {
            Employee employee = new Employee();

            Console.Write("Inserisci il nome: ");
            employee.Name = Console.ReadLine();
            Console.Write("Inserisci il genere: ");
            employee.Gender = Console.ReadLine();
            Console.Write("Inserisci il dipartimento: ");
            employee.Department = Console.ReadLine();
            Console.Write("Inserisci la città: ");
            employee.City = Console.ReadLine();

            employeeDAL.AddEmployee(employee);
            Console.WriteLine("Utente creato con successo.");
        }

        static void VisualizzaUtente(EmployeeDataAccessLayer employeeDAL)
        {
            Console.Write("Inserisci l'ID dell'utente: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee employee = employeeDAL.GetEmployeeData(id);

            if (employee != null)
            {
                Console.WriteLine("ID: " + employee.ID);
                Console.WriteLine("Nome: " + employee.Name);
                Console.WriteLine("Genere: " + employee.Gender);
                Console.WriteLine("Dipartimento: " + employee.Department);
                Console.WriteLine("Città: " + employee.City);
            }
            else
            {
                Console.WriteLine("Utente non trovato.");
            }
        }

        static void AggiornaUtente(EmployeeDataAccessLayer employeeDAL)
        {
            Console.Write("Inserisci l'ID dell'utente: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee employee = employeeDAL.GetEmployeeData(id);

            if (employee != null)
            {
                Console.Write("Inserisci il nuovo nome (lascia vuoto per mantenere l'attuale): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name)) employee.Name = name;

                Console.Write("Inserisci il nuovo genere (lascia vuoto per mantenere l'attuale): ");
                string gender = Console.ReadLine();
                if (!string.IsNullOrEmpty(gender)) employee.Gender = gender;

                Console.Write("Inserisci il nuovo dipartimento (lascia vuoto per mantenere l'attuale): ");
                string department = Console.ReadLine();
                if (!string.IsNullOrEmpty(department)) employee.Department = department;

                Console.Write("Inserisci la nuova città (lascia vuoto per mantenere l'attuale): ");
                string city = Console.ReadLine();
                if (!string.IsNullOrEmpty(city)) employee.City = city;

                employeeDAL.UpdateEmployee(employee);
                Console.WriteLine("Utente aggiornato con successo.");
            }
            else
            {
                Console.WriteLine("Utente non trovato.");
            }
        }

        static void ListaTuttiGliUtenti(EmployeeDataAccessLayer employeeDAL)
        {
            var employees = employeeDAL.GetAllEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine("ID: " + employee.ID);
                Console.WriteLine("Nome: " + employee.Name);
                Console.WriteLine("Genere: " + employee.Gender);
                Console.WriteLine("Dipartimento: " + employee.Department);
                Console.WriteLine("Città: " + employee.City);
                Console.WriteLine();
            }
        }

        static void EliminaUtente(EmployeeDataAccessLayer employeeDAL)
        {
            Console.Write("Inserisci l'ID dell'utente da eliminare: ");
            int id = Convert.ToInt32(Console.ReadLine());

            employeeDAL.DeleteEmployee(id);
            Console.WriteLine("Utente eliminato con successo.");
        }
    }
    
}
