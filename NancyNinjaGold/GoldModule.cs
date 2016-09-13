using System;
using System.Collections.Generic;
using Nancy;

namespace NancyNinjaGold
{

    public class GoldModule : NancyModule
    {

        public GoldModule()
        {
            Get("/", args =>
            {
                if(Session["Gold"] == null)
                {
                    Session["Gold"] = 0;
                    Session["Activities"] = new List<string>();
                }
                ViewBag.Gold = Session["Gold"];

                return View["index", Session["Activities"]];

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
                Session["Gold"] = (int)Session["Gold"] + GoldEarned;

                int AbsGoldEarned = Math.Abs(GoldEarned);
                string activity = "";
                if(GoldEarned < 0)
                {
                    activity = $"Entered the {Building} and lost {AbsGoldEarned} gold!";
                }
                else
                {
                    activity = $"Entered the {Building} and made {AbsGoldEarned} gold!";
                }

                ((List<string>)Session["Activities"]).Insert(0, activity);

                return Response.AsRedirect("/");
            });

            Get("/reset", args =>
            {
                Session.DeleteAll();

                return Response.AsRedirect("/");
            });
        }
    }
}