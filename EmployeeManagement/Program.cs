using System;
using System.Collections.Generic;
using System.Diagnostics;
using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EmployeeDataAccessLayerWithDAO employeeDAL_ADO = new EmployeeDataAccessLayerWithDAO();
            EmployeeDataAccessLayerWithDapper employeeDAL_Dapper = new EmployeeDataAccessLayerWithDapper();

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
                        CreaUtente(employeeDAL_ADO, employeeDAL_Dapper);
                        break;
                    case 2:
                        VisualizzaUtente(employeeDAL_ADO, employeeDAL_Dapper);
                        break;
                    case 3:
                        AggiornaUtente(employeeDAL_ADO, employeeDAL_Dapper);
                        break;
                    case 4:
                        ListaTuttiGliUtenti(employeeDAL_ADO, employeeDAL_Dapper);
                        break;
                    case 5:
                        EliminaUtente(employeeDAL_ADO, employeeDAL_Dapper);
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }
            }
        }

        static void CreaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper)
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

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            employeeDAL_ADO.AddEmployee(employee);
            stopwatch.Stop();
            Console.WriteLine("Utente creato con successo usando ADO.NET in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_Dapper.AddEmployee(employee);
            stopwatch.Stop();
            Console.WriteLine("Utente creato con successo usando Dapper in " + stopwatch.ElapsedMilliseconds + " ms.");
        }

        static void VisualizzaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper)
        {
            Console.Write("Inserisci l'ID dell'utente: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Employee employee_ADO = employeeDAL_ADO.GetEmployeeData(id);
            stopwatch.Stop();
            Console.WriteLine("Dati utente usando ADO.NET ottenuti in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaDatiUtente(employee_ADO);

            stopwatch.Reset();
            stopwatch.Start();
            Employee employee_Dapper = employeeDAL_Dapper.GetEmployeeData(id);
            stopwatch.Stop();
            Console.WriteLine("Dati utente usando Dapper ottenuti in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaDatiUtente(employee_Dapper);
        }

        static void AggiornaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper)
        {
            Console.Write("Inserisci l'ID dell'utente: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee employee_ADO = employeeDAL_ADO.GetEmployeeData(id);
            Employee employee_Dapper = employeeDAL_Dapper.GetEmployeeData(id);

            if (employee_ADO != null && employee_Dapper != null)
            {
                Console.Write("Inserisci il nuovo nome (lascia vuoto per mantenere l'attuale): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    employee_ADO.Name = name;
                    employee_Dapper.Name = name;
                }

                Console.Write("Inserisci il nuovo genere (lascia vuoto per mantenere l'attuale): ");
                string gender = Console.ReadLine();
                if (!string.IsNullOrEmpty(gender))
                {
                    employee_ADO.Gender = gender;
                    employee_Dapper.Gender = gender;
                }

                Console.Write("Inserisci il nuovo dipartimento (lascia vuoto per mantenere l'attuale): ");
                string department = Console.ReadLine();
                if (!string.IsNullOrEmpty(department))
                {
                    employee_ADO.Department = department;
                    employee_Dapper.Department = department;
                }

                Console.Write("Inserisci la nuova città (lascia vuoto per mantenere l'attuale): ");
                string city = Console.ReadLine();
                if (!string.IsNullOrEmpty(city))
                {
                    employee_ADO.City = city;
                    employee_Dapper.City = city;
                }

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                employeeDAL_ADO.UpdateEmployee(employee_ADO);
                stopwatch.Stop();
                Console.WriteLine("Utente aggiornato con successo usando ADO.NET in " + stopwatch.ElapsedMilliseconds + " ms.");

                stopwatch.Reset();
                stopwatch.Start();
                employeeDAL_Dapper.UpdateEmployee(employee_Dapper);
                stopwatch.Stop();
                Console.WriteLine("Utente aggiornato con successo usando Dapper in " + stopwatch.ElapsedMilliseconds + " ms.");
            }
            else
            {
                Console.WriteLine("Utente non trovato.");
            }
        }

        static void ListaTuttiGliUtenti(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var employees_ADO = employeeDAL_ADO.GetAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Lista utenti usando ADO.NET ottenuta in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaListaUtenti(employees_ADO);

            stopwatch.Reset();
            stopwatch.Start();
            var employees_Dapper = employeeDAL_Dapper.GetAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Lista utenti usando Dapper ottenuta in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaListaUtenti(employees_Dapper);
        }

        static void EliminaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper)
        {
            Console.Write("Inserisci l'ID dell'utente da eliminare: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            employeeDAL_ADO.DeleteEmployee(id);
            stopwatch.Stop();
            Console.WriteLine("Utente eliminato con successo usando ADO.NET in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_Dapper.DeleteEmployee(id);
            stopwatch.Stop();
            Console.WriteLine("Utente eliminato con successo usando Dapper in " + stopwatch.ElapsedMilliseconds + " ms.");
        }

        static void StampaDatiUtente(Employee employee)
        {
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

        static void StampaListaUtenti(IEnumerable<Employee> employees)
        {
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
    }
}
