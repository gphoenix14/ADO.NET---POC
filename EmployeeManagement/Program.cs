using System;
using System.Collections.Generic;
using System.Diagnostics;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<EmployeeContext>(options =>
                    options.UseMySql("Server=localhost;Database=employee;User Id=employee;Password=employee;",
                        new MySqlServerVersion(new Version(8, 0, 21))))
                .AddScoped<EmployeeDataAccessLayerWithEF>()
                .BuildServiceProvider();

            EmployeeDataAccessLayerWithDAO employeeDAL_ADO = new EmployeeDataAccessLayerWithDAO();
            EmployeeDataAccessLayerWithDapper employeeDAL_Dapper = new EmployeeDataAccessLayerWithDapper();
            EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate = new EmployeeDataAccessLayerWithNHibernate();
            var employeeDAL_EF = serviceProvider.GetService<EmployeeDataAccessLayerWithEF>();

            while (true)
            {
                Console.WriteLine("Seleziona un'opzione:");
                Console.WriteLine("1. Crea un utente");
                Console.WriteLine("2. Visualizza dati utente (inserendo un ID)");
                Console.WriteLine("3. Aggiorna un utente (inserendo l'ID)");
                Console.WriteLine("4. Lista tutti gli utenti");
                Console.WriteLine("5. Elimina un utente (fornendo il suo ID)");
                Console.WriteLine("6. Esci");
                Console.WriteLine("7. Azzera la tabella Employee");

                int scelta = Convert.ToInt32(Console.ReadLine());

                switch (scelta)
                {
                    case 1:
                        CreaUtente(employeeDAL_ADO, employeeDAL_Dapper, employeeDAL_NHibernate, employeeDAL_EF);
                        break;
                    case 2:
                        VisualizzaUtente(employeeDAL_ADO, employeeDAL_Dapper, employeeDAL_NHibernate, employeeDAL_EF);
                        break;
                    case 3:
                        AggiornaUtente(employeeDAL_ADO, employeeDAL_Dapper, employeeDAL_NHibernate, employeeDAL_EF);
                        break;
                    case 4:
                        ListaTuttiGliUtenti(employeeDAL_ADO, employeeDAL_Dapper, employeeDAL_NHibernate, employeeDAL_EF);
                        break;
                    case 5:
                        EliminaUtente(employeeDAL_ADO, employeeDAL_Dapper, employeeDAL_NHibernate, employeeDAL_EF);
                        break;
                    case 6:
                        return;
                    case 7:
                        AzzeraTabellaEmployee(employeeDAL_ADO, employeeDAL_Dapper, employeeDAL_NHibernate, employeeDAL_EF);
                        break;
                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }
            }
        }

        static void CreaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper, EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate, EmployeeDataAccessLayerWithEF employeeDAL_EF)
        {
            EmployeeEF employee = new EmployeeEF();

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
            employeeDAL_ADO.AddEmployee(new Employee
            {
                Name = employee.Name,
                Gender = employee.Gender,
                Department = employee.Department,
                City = employee.City
            });
            stopwatch.Stop();
            Console.WriteLine("Utente creato con successo usando ADO.NET in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_Dapper.AddEmployee(new Employee
            {
                Name = employee.Name,
                Gender = employee.Gender,
                Department = employee.Department,
                City = employee.City
            });
            stopwatch.Stop();
            Console.WriteLine("Utente creato con successo usando Dapper in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_NHibernate.AddEmployee(new Employee
            {
                Name = employee.Name,
                Gender = employee.Gender,
                Department = employee.Department,
                City = employee.City
            });
            stopwatch.Stop();
            Console.WriteLine("Utente creato con successo usando NHibernate in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_EF.AddEmployee(employee);
            stopwatch.Stop();
            Console.WriteLine("Utente creato con successo usando Entity Framework in " + stopwatch.ElapsedMilliseconds + " ms.");
        }

        static void VisualizzaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper, EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate, EmployeeDataAccessLayerWithEF employeeDAL_EF)
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

            stopwatch.Reset();
            stopwatch.Start();
            Employee employee_NHibernate = employeeDAL_NHibernate.GetEmployeeData(id);
            stopwatch.Stop();
            Console.WriteLine("Dati utente usando NHibernate ottenuti in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaDatiUtente(employee_NHibernate);

            stopwatch.Reset();
            stopwatch.Start();
            EmployeeEF employee_EF = employeeDAL_EF.GetEmployeeData(id);
            stopwatch.Stop();
            Console.WriteLine("Dati utente usando Entity Framework ottenuti in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaDatiUtente(employee_EF);
        }

        static void AggiornaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper, EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate, EmployeeDataAccessLayerWithEF employeeDAL_EF)
        {
            Console.Write("Inserisci l'ID dell'utente: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee employee_ADO = employeeDAL_ADO.GetEmployeeData(id);
            Employee employee_Dapper = employeeDAL_Dapper.GetEmployeeData(id);
            Employee employee_NHibernate = employeeDAL_NHibernate.GetEmployeeData(id);
            EmployeeEF employee_EF = employeeDAL_EF.GetEmployeeData(id);

            if (employee_ADO != null && employee_Dapper != null && employee_NHibernate != null && employee_EF != null)
            {
                Console.Write("Inserisci il nuovo nome (lascia vuoto per mantenere l'attuale): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    employee_ADO.Name = name;
                    employee_Dapper.Name = name;
                    employee_NHibernate.Name = name;
                    employee_EF.Name = name;
                }

                Console.Write("Inserisci il nuovo genere (lascia vuoto per mantenere l'attuale): ");
                string gender = Console.ReadLine();
                if (!string.IsNullOrEmpty(gender))
                {
                    employee_ADO.Gender = gender;
                    employee_Dapper.Gender = gender;
                    employee_NHibernate.Gender = gender;
                    employee_EF.Gender = gender;
                }

                Console.Write("Inserisci il nuovo dipartimento (lascia vuoto per mantenere l'attuale): ");
                string department = Console.ReadLine();
                if (!string.IsNullOrEmpty(department))
                {
                    employee_ADO.Department = department;
                    employee_Dapper.Department = department;
                    employee_NHibernate.Department = department;
                    employee_EF.Department = department;
                }

                Console.Write("Inserisci la nuova città (lascia vuoto per mantenere l'attuale): ");
                string city = Console.ReadLine();
                if (!string.IsNullOrEmpty(city))
                {
                    employee_ADO.City = city;
                    employee_Dapper.City = city;
                    employee_NHibernate.City = city;
                    employee_EF.City = city;
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

                stopwatch.Reset();
                stopwatch.Start();
                employeeDAL_NHibernate.UpdateEmployee(employee_NHibernate);
                stopwatch.Stop();
                Console.WriteLine("Utente aggiornato con successo usando NHibernate in " + stopwatch.ElapsedMilliseconds + " ms.");

                stopwatch.Reset();
                stopwatch.Start();
                employeeDAL_EF.UpdateEmployee(employee_EF);
                stopwatch.Stop();
                Console.WriteLine("Utente aggiornato con successo usando Entity Framework in " + stopwatch.ElapsedMilliseconds + " ms.");
            }
            else
            {
                Console.WriteLine("Utente non trovato.");
            }
        }

        static void ListaTuttiGliUtenti(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper, EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate, EmployeeDataAccessLayerWithEF employeeDAL_EF)
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

            stopwatch.Reset();
            stopwatch.Start();
            var employees_NHibernate = employeeDAL_NHibernate.GetAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Lista utenti usando NHibernate ottenuta in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaListaUtenti(employees_NHibernate);

            stopwatch.Reset();
            stopwatch.Start();
            var employees_EF = employeeDAL_EF.GetAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Lista utenti usando Entity Framework ottenuta in " + stopwatch.ElapsedMilliseconds + " ms.");
            StampaListaUtenti(employees_EF);
        }

        static void EliminaUtente(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper, EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate, EmployeeDataAccessLayerWithEF employeeDAL_EF)
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

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_NHibernate.DeleteEmployee(id);
            stopwatch.Stop();
            Console.WriteLine("Utente eliminato con successo usando NHibernate in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_EF.DeleteEmployee(id);
            stopwatch.Stop();
            Console.WriteLine("Utente eliminato con successo usando Entity Framework in " + stopwatch.ElapsedMilliseconds + " ms.");
        }

        static void AzzeraTabellaEmployee(EmployeeDataAccessLayerWithDAO employeeDAL_ADO, EmployeeDataAccessLayerWithDapper employeeDAL_Dapper, EmployeeDataAccessLayerWithNHibernate employeeDAL_NHibernate, EmployeeDataAccessLayerWithEF employeeDAL_EF)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            employeeDAL_ADO.DeleteAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Tabella azzerata con successo usando ADO.NET in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_Dapper.DeleteAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Tabella azzerata con successo usando Dapper in " + stopwatch.ElapsedMilliseconds + " ms.");

            stopwatch.Reset();
            stopwatch.Start();
            employeeDAL_NHibernate.DeleteAllEmployees();
            stopwatch.Stop();
            Console.WriteLine("Tabella azzerata con successo usando NHibernate in " + stopwatch.ElapsedMilliseconds + " ms.");
    
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

        static void StampaDatiUtente(EmployeeEF employee)
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

        static void StampaListaUtenti(IEnumerable<EmployeeEF> employees)
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
