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
                    Query = "Vad �r f�retagsekonomi?",
                    Answer = "F�retagsekonomi �r l�ran om f�retags hush�llande med begr�nsade resurser. " +
                             "Det brukar anv�ndas som det �vergripande namnet p� en rad subdiscipliner relaterade till f�retag " +
                             "i en vidare bem�rkelse, som �ven omfattar �tminstone formellt icke vinstinriktade organisationer."
                },
                new Question
                {
                    Query = "Vad �r analytisk f�retagsekonomi?",
                    Answer = "Analytisk f�retagsekonomi handlar ocks� om att finna optimala l�sningar p� " +
                             "f�retagsekonomiska problem s�som hur stora och hur m�nga butiker ett handelsf�retag b�r ha, " +
                             "vad som �r en l�mplig styrelsesammans�ttning (antal ledam�ter, deras erfarenheter, k�n, mm),"
                }

            };

            var lawQuestions = new List<Question>
            {
                new Question
                {
                    Query = "Vad �r juridik?",
                    Answer =
                        "Juridik �r l�ran om r�ttsreglernas tolkning och till�mpning. Den som har avlagt en juristexamen kallas jurist."
                },
                new Question
                {
                    Query = "I vilka r�ttsomr�den kan juridik delas in i?",
                    Answer = "Juridiken delas in i olika r�ttsomr�den, beroende p� vad som regleras. Den offentliga r�tten reglerar f�rh�llandet mellan enskilda personer och det allm�nna, " +
                             "medan f�rh�llandet mellan privata r�ttssubjekt regleras genom civilr�tten. " +
                             "Gr�nserna �r inte knivskarpa utan vissa civilr�ttsliga omr�den kan ha offentligr�ttsliga inslag, exempelvis att fastigheter regleras genom offentliga f�rfaranden, " +
                             "liksom vissa offentligr�ttsliga omr�den kan ha civilr�ttsliga inslag, exempelvis att konkurrensr�tten har betydelse f�r utformningen av civilr�ttsliga avtal."
                },
                new Question
                {
                    Query = "Vad �r havsr�tt?",
                    Answer = "Havsr�tt �r en del av den internationella r�tten som styr bland annat staters r�tt till olika havsomr�den. Havsr�tten regleras bland annat i Havsr�ttskonventionen fr�n 1982."
                },
                new  Question()
                {
                    Query = "Vad �r konkurrensr�tt?",
                    Answer = "onkurrensr�tt �r en gemensam klassificering f�r de r�ttsomr�den som reglerar ramarna f�r konkurrens och hur staten, " +
                             "f�retag eller personer som bedriver en ekonomisk verksamhet f�r agera. R�ttsomr�det innefattar bland " +
                             "annat hur f�retag kan samverka och m�jligheterna att k�pa och s�lja f�retag, fr�gor om monopol, offentliga upphandlingar och statsst�dsregler inom EU.[1]"
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
