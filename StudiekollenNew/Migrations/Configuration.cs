using System.Collections.Generic;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Models;

namespace StudiekollenNew.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudiekollenNew.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {

            var economiQuestions = new List<Question>
            {
                new Question
                {
                    Query = "Vad är företagsekonomi?",
                    Answer = "Företagsekonomi är läran om företags hushållande med begränsade resurser. " +
                             "Det brukar användas som det övergripande namnet på en rad subdiscipliner relaterade till företag " +
                             "i en vidare bemärkelse, som även omfattar åtminstone formellt icke vinstinriktade organisationer."
                },
                new Question
                {
                    Query = "Vad är analytisk företagsekonomi?",
                    Answer = "Analytisk företagsekonomi handlar också om att finna optimala lösningar på " +
                             "företagsekonomiska problem såsom hur stora och hur många butiker ett handelsföretag bör ha, " +
                             "vad som är en lämplig styrelsesammansättning (antal ledamöter, deras erfarenheter, kön, mm),"
                }

            };

            var lawQuestions = new List<Question>
            {
                new Question
                {
                    Query = "Vad är juridik?",
                    Answer =
                        "Juridik är läran om rättsreglernas tolkning och tillämpning. Den som har avlagt en juristexamen kallas jurist."
                },
                new Question
                {
                    Query = "I vilka rättsområden kan juridik delas in i?",
                    Answer = "Juridiken delas in i olika rättsområden, beroende på vad som regleras. Den offentliga rätten reglerar förhållandet mellan enskilda personer och det allmänna, " +
                             "medan förhållandet mellan privata rättssubjekt regleras genom civilrätten. " +
                             "Gränserna är inte knivskarpa utan vissa civilrättsliga områden kan ha offentligrättsliga inslag, exempelvis att fastigheter regleras genom offentliga förfaranden, " +
                             "liksom vissa offentligrättsliga områden kan ha civilrättsliga inslag, exempelvis att konkurrensrätten har betydelse för utformningen av civilrättsliga avtal."
                },
                new Question
                {
                    Query = "Vad är havsrätt?",
                    Answer = "Havsrätt är en del av den internationella rätten som styr bland annat staters rätt till olika havsområden. Havsrätten regleras bland annat i Havsrättskonventionen från 1982."
                },
                new  Question()
                {
                    Query = "Vad är konkurrensrätt?",
                    Answer = "onkurrensrätt är en gemensam klassificering för de rättsområden som reglerar ramarna för konkurrens och hur staten, " +
                             "företag eller personer som bedriver en ekonomisk verksamhet får agera. Rättsområdet innefattar bland " +
                             "annat hur företag kan samverka och möjligheterna att köpa och sälja företag, frågor om monopol, offentliga upphandlingar och statsstödsregler inom EU.[1]"
                }
            };

            context.Exam.AddOrUpdate(
                new Exam
                {
                    ExamName = "Ekonomi",
                    ExamTime = new TimeSpan(0,2,30,0),
                    SendReminderDate = DateTime.Now.Add(new TimeSpan(2,3,0,0)),
                    RandomOrder = false,
                    UserId = "1a73d7e9-138b-4704-9cee-e34138d24c00",
                    Questions = economiQuestions
                },
                
                new Exam
                {
                    ExamName = "Juridik",
                    RandomOrder = true,
                    UserId = "1a73d7e9-138b-4704-9cee-e34138d24c00",
                    Questions = lawQuestions

                }



            );

            base.Seed(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
