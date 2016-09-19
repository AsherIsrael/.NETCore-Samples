using System;
using System.Collections.Generic;
using DbConnection;
using Nancy;

namespace NancyQuotingDojo
{
    public class QuotesModule : NancyModule
    {
        public QuotesModule()
        {

            Get("/", args =>
            {
                Console.WriteLine("new");
                List<string> Errors = new List<string>();
                if(Session["Errors"] != null)
                {
                    Errors = (List<string>)Session["Errors"];
                }
                return View["new", Errors];
            });

            Post("/quotes", args =>
            {
                Console.WriteLine("create");
                string name = (string)Request.Form.name;
                string quote = (string)Request.Form.quote;

                List<string> Errors = new List<string>();
                if(name.Length < 1)
                {
                    Errors.Add("Name cannot be blank");
                }
                if(quote.Length < 1)
                {
                    Errors.Add("Quote cannot be blank");
                }
                if(Errors.Count > 0)
                {
                    Session["Errors"] = Errors;
                    return Response.AsRedirect("/");
                }
                else
                {
                    string query = $"INSERT INTO quote (Name, QuoteText, createdAt) VALUES ('{name}', '{quote}', NOW())";
                    Console.WriteLine(query);
                    DbConnector.ExecuteQuery(query);
                    return Response.AsRedirect("/quotes");
                }
            });

            Get("/quotes", args =>
            {
                Console.WriteLine("index");
                string query = "SELECT * FROM quote ORDER BY CreatedAt DESC";
                List<Dictionary<string, object>> quotes = DbConnector.ExecuteQuery(query);
                return View["index", quotes];
            });
        }
    }
}