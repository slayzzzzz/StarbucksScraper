﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarbucksScraper;
using System.Configuration;
using Soda2Consumer;
using Soda2Publisher;

namespace SocrataUploader
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new StoresEntities())
            {

                string host = ConfigurationManager.AppSettings["SocrataHost"];
                string datasetId = ConfigurationManager.AppSettings["SocrataDatasetID"];
                string username = ConfigurationManager.AppSettings["SocrataUsername"];
                string password = ConfigurationManager.AppSettings["SocrataPassword"];
                string appToken = ConfigurationManager.AppSettings["SocrataAppToken"];

                var basicAuthClient = new Soda2Client(username, password, appToken);
                var dataset = basicAuthClient.getDatasetInfo<Row>(host, datasetId);

                Console.WriteLine("Truncating");
                dataset.truncate();

                LinkedList<Row> rows = new LinkedList<Row>();

                Row row;
                foreach (var store in db.Stores)
                {
                    var streetCombined = "";

                    if (store.Street1 != null && store.Street1.Trim() != "")
                    {
                        streetCombined = streetCombined + store.Street1.Trim();
                    }

                    if (store.Street2 != null && store.Street2.Trim() != "")
                    {
                        streetCombined = streetCombined + ", " + store.Street2.Trim();
                    }

                    if (store.Street3 != null && store.Street3.Trim() != "")
                    {
                        streetCombined = streetCombined + ", " + store.Street3.Trim();
                    }

                    row = new Row();
                    row.Add("Store ID", store.StarbucksStoreID);
                    row.Add("Name", store.Name);
                    row.Add("Brand", store.BrandName);
                    row.Add("Store Number", store.StoreNumber);
                    row.Add("Phone Number", store.PhoneNumber);
                    row.Add("Ownership Type", store.OwnershipType);
                    row.Add("Street Combined", streetCombined);
                    row.Add("Street 1", store.Street1);
                    row.Add("Street 2", store.Street2);
                    row.Add("Street 3", store.Street3);
                    row.Add("City", store.City);
                    row.Add("Country Subdivision", store.CountrySubdivisionCode);
                    row.Add("Country", store.CountryCode);
                    row.Add("Postal Code", store.PostalCode);
                    row.Add("Latitude", store.Latitude);
                    row.Add("Longitude", store.Longitude);
                    row.Add("Timezone", store.TZID);
                    row.Add("Current Timezone Offset", store.TZOffset);
                    row.Add("Olson Timezone", store.TZOlsonID);

                    rows.AddLast(row);

                    Console.WriteLine("Added store " + store.StarbucksStoreID);

                    if (rows.Count > 2000)
                    {
                        Console.WriteLine("Upserting batch");
                        dataset.upsert(rows.ToArray());
                        rows.Clear();
                    }
                    
                }

                if (rows.Count > 0)
                {
                    Console.WriteLine("Upserting last");
                    dataset.upsert(rows.ToArray());
                }

            }

        }
    }
}
