using System;
using Nancy;

namespace NancyNumberGame
{
    public class NumberModule : NancyModule
    {
        public NumberModule()
        {

            Get("/", args =>
            {
                Console.WriteLine("index");
                if(Session["TheNumber"] == null)
                {
                    Console.WriteLine("We're here!");
                    int Num = new Random().Next(1, 100);
                    SessionWrapper box = new SessionWrapper();
                    box.Number = Num;
                    // object box = (object)Convert.ChangeType(Num, typeof(object));
                    
                    Session["TheNumber"] = box;
                }
                // Console.WriteLine(Session["TheNumber"]);
                // Console.WriteLine(Session["TheNumber"].GetType() == typeof(int));
                // Console.WriteLine(Session["TheNumber"].GetType());

                if(Session["result"] == null)
                {
                    Console.WriteLine("result is null");
                    ViewBag.LastGuess = "";
                    ViewBag.Class = "";
                    ViewBag.Restart = false;
                }
                else
                {
                    Console.WriteLine("result has been set");
                    ViewBag.LastGuess = Session["result"];
                    if((string)Session["result"] == "You Got It!"){
                        ViewBag.Class = "correct";
                        ViewBag.Restart = true;
                    }else{
                        ViewBag.Class = "wrong";
                        ViewBag.Restart = false;
                    }
                }

                // ViewBag.TheNumber = Session["TheNumber"];
                // ViewBag.TheNumber = (Session["TheNumber"] as SessionWrapper).Number;

                return View["guess"];
            });

            Post("/guess", args =>
            {
                int TheNumber = (Session["TheNumber"] as SessionWrapper).Number;
                Console.WriteLine("guessing");
                var guess = (int)Request.Form.Number;
                if(guess > TheNumber)
                {
                    Session["result"] = "Too High";
                }
                else if(guess < TheNumber)
                {
                    Session["result"] = "Too Low";
                }
                else
                {
                    Session["result"] = "You Got It!";
                }

                return Response.AsRedirect("/");
            });


            Get("/reset", args =>
            {
                Console.WriteLine("resetting");
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });
        }
    }
}