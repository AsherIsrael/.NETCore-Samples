using System;
// using System.Collections.Generic;
using Nancy;

namespace NancyNinjaGold
{
    public class GoldModule : NancyModule
    {

        public GoldModule()
        {
            Get("/", args =>
            {
                if(Session["Info"] == null)
                {
                    Console.WriteLine("In here");
                    Session["Info"] = new SessionObject();
                    // (Session["Info"] as SessionObject).Gold = 0;
                    // (Session["Info"] as SessionObject).Activities = new List<string>();
                }
                ViewBag.Info = (SessionObject)Session["Info"];
                Console.WriteLine(((SessionObject)Session["Info"]).Gold);
                return View["index"];
            });
            
            Post("/process_money", args =>
            {
                string Building = Request.Form.Building;

                Random Rand = new Random();
                int GoldEarned = 0;

                switch(Building)
                {
                    case "farm":
                        GoldEarned = Rand.Next(10, 20);
                        break;
                    case "cave":
                        GoldEarned = Rand.Next(5, 10);
                        break;
                    case "house":
                        GoldEarned = Rand.Next(2, 5);
                        break;
                    case "casino":
                        GoldEarned = Rand.Next(-50, 50);
                        break;
                }
                // Console.WriteLine(GoldEarned);
                ((SessionObject)Session["Info"]).Gold += GoldEarned;
                // Console.WriteLine((Session["Info"] as SessionObject).Gold);

                string activity = "";

                if(GoldEarned < 0)
                {
                    activity = $"Entered the {Building} and lost {GoldEarned} gold!";
                }
                else
                {
                    activity = $"Entered the {Building} and made {GoldEarned} gold!";
                }

                (Session["Info"] as SessionObject).Activities.Add(activity);


                return Response.AsRedirect("/");
            });
        }
    }
}