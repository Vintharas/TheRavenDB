/*
 *  Code samples for
 *  "Edgar Allan Poe
 *    presents
 *      The Raven DB"
 *  
 *  The .NET API, Basic operations
 *      - Create a document store
 *      - Save an entity
 *      - Query an entity by id
 *      
 *      - To Add
 *        - Rewrite example in terms of The Raven xD
 *        - Id generation strategies
 *        - Inheritance
 *
 * 
 *  by Jaime Gonzalez Garcia
 * 
 */

using System;
using System.Linq;
using Raven.Client.Document;

namespace TheRavenDB.ConsoleSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "test"
            }.Initialize())
            {
                string companyId;

                // Saving an entity
                // 
                // - All writes in RavenDB are batched
                // - Safe by Default
                //   - doesn't allow unbounded results. 
                //      - If a page size value is not specified, the length of the results will be limited to 128 results. 
                //      - On server side, hard limit on page size of 1,024 results (configurable).
                //   - avoid N+1
                //      - number of remote calles to server per sesion is limited to 30 (configurable)

                using (var session = documentStore.OpenSession())
                {
                    var company = new Company { Name = "The Raven" };
                    session.Store(company);
                    session.SaveChanges();
                    companyId = company.Id;
                    Console.WriteLine(string.Format("1. Saved company {0}", company.Name));
                }

                // Querying by id
                using (var session = documentStore.OpenSession())
                {
                    var company = session.Load<Company>(companyId);
                    Console.WriteLine(string.Format("2. Check this company {0}, with id {1}", company.Name, company.Id));
                }

                // Modify document
                using (var session = documentStore.OpenSession())
                {
                    var company = session.Load<Company>(companyId);
                    company.Address = new Address
                    {
                        City = "Kansas" // we moved to Kansas!
                    };
                    session.SaveChanges();
                    Console.WriteLine(string.Format("3. Modified company {0}", company.Name));
                }

                // Delete
                //using (var session = documentStore.OpenSession())
                //{
                //    var company = session.Load<Company>(companyId);
                //    session.Delete(company);
                //    session.SaveChanges();
                //    Console.WriteLine(string.Format("4. Removed company {0}", company.Name));
                //}

                // Delete by id - executes immediately on the DB
                // documentStore.DatabaseCommands.Delete(companyId, null);

                // Query with LINQ
                using (var session = documentStore.OpenSession())
                {
                    var companiesInKansas = session.Query<Company>()
                        .Where(c => c.Address.City == "Kansas")
                        .ToList();

                    Console.WriteLine("4. Which companies are located in Kansas?");
                    foreach (var company in companiesInKansas)
                        Console.WriteLine(string.Format("{0} in {1} ", company.Name, company.Address.City));

                }

                DoAsyncStuff();

            }
            
            
        }

        public static async void DoAsyncStuff()
        {

            Console.WriteLine("Doing async stuff!!");
            using (var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "test"

            }.Initialize())
            {
                 // Did you see this support for async?
                using (var asyncSession = documentStore.OpenAsyncSession())
                {
                    // loading an entity asyn
                    var anotherCompany = await asyncSession.LoadAsync<Company>(1); 

                    // storing an entity async
                    var company = new Company {Name = "Async Company"};
                    await asyncSession.StoreAsync(company);
                    await asyncSession.SaveChangesAsync();
                }
            }

    }
    }


    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }


}
