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
                if(Session["TheNumber"] == null)
                {
                    int Num = new Random().Next(1, 100);
                    SessionWrapper box = new SessionWrapper();
                    box.Number = Num;
                    
                    Session["TheNumber"] = box;
                }

                if(Session["result"] == null)
                {
                    ViewBag.LastGuess = "";
                    ViewBag.Class = "";
                    ViewBag.Restart = false;
                }
                else
                {
                    ViewBag.LastGuess = Session["result"];
                    if((string)Session["result"] == "You Got It!"){
                        ViewBag.Class = "correct";
                        ViewBag.Restart = true;
                    }else{
                        ViewBag.Class = "wrong";
                        ViewBag.Restart = false;
                    }
                }

                return View["guess"];
            });

            Post("/guess", args =>
            {
                int TheNumber = (Session["TheNumber"] as SessionWrapper).Number;
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
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });
        }
    }
}