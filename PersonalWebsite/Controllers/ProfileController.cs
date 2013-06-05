using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PersonalWebsite.Classes;
using PersonalWebsite.Models.Profile;

namespace PersonalWebsite.Controllers
{
    public partial class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public virtual ActionResult Index()
        {            
            return View();
        }

        #region ChildActions
        [ChildActionOnly]
        public virtual PartialViewResult AboutMe()
        {
            var model = new AboutMeViewModel();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Programador autoditada com experiência no uso de uma grande variedade de linguagens de programação.</p>");
            sb.Append("<p>Nos dias autais eu me concentro em C# ASP.NET utilizando MVC4 e EF voltados para soluções mobile e em nuvem (Azure).</p>");
            sb.Append("<p>Também possuo habilidades em design, com experiência em codificação de projetos usando HTML / CSS e utilizando jQuery, também com o auxílio de softwares de design como Adobe Photoshop.</p>");            
            model.Blurb = sb.ToString();

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult PieCharts()
        {            
            return PartialView();
        }

        [ChildActionOnly]
        public virtual PartialViewResult CoderbitsFooter()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public virtual PartialViewResult Skills()
        {
            var model = new SkillsViewModel();

            var primarySkills = new List<Skill>();
            var secondarySkills = new List<Skill>();

            // primary skills
            primarySkills.Add(new Skill(70, "C#",
                @"Conhecimento: Moderado<br>" + 
                "Experiência: 2 anos<br><br>" +
                "Linguagem preferida para desenvolvimento de software e web. Tecnologias incluem MSSQL, MVC, EF, Azure, Mobile, etc"
            ));
            primarySkills.Add(new Skill(70, "ASP.NET",
                "Conhecimento: Moderado<br>" +
                "Experiência: 2 anos<br><br>" +
                "Ambiente preferido para desenvolvimento web."
            ));
            primarySkills.Add(new Skill(60, "MVC",
                "Conhecimento: Sólido<br>" +
                "Experiência: 1 ano<br><br>" +
                "O foco do meu aprendizado nos últimos anos."
            ));

            // secondary skills
            secondarySkills.Add(new Skill(80, "HTML",
                "Conhecimento: Avançado<br>" +
                "Experiência: 3 anos<br><br>" +
                "Linguagem mais usada em toda a minha carreira quando considerados os projetos web"
            ));
            secondarySkills.Add(new Skill(70, "CSS",
                "Conhecimento: Moderado<br>" +
                "Experiência: 3 anos<br><br>" +
                "Juntamente com HTML fez parte da minha trajetória desde o início"
            ));
            secondarySkills.Add(new Skill(50, "JS",
                "Conhecimento: Bom<br>" +
                "Experiência: 2 anos<br><br>" +
                "Meu uso de JavaScript tem sido esporárido. Não é o meu foco porém tenho obtido sucesso em implementações jQuery e javascript"
            ));
            secondarySkills.Add(new Skill(60, "PHP",
                "Conhecimento: Sólido<br>" +
                "Experiência: 3 anos<br><br>" +
                "Primeira linguagem de programação web que tive contato. Tenho um conhecimento sólido, porém atualmente foco mais em .NET"
            ));
            secondarySkills.Add(new Skill(60, "SQL",
                "Conhecimento: Sólido<br>" +
                "Experiência: 2 anos<br><br>" +
                "Tenho um conhecimento sólido de consultas-SQL e administração, assim como MySQL, Oracle e DB2 (multibanco)"
            ));

            model.PrimarySkills = primarySkills;
            model.SecondarySkills = secondarySkills;

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult Traits()
        {
            var model = new TraitsViewModel();

            var traits = new List<Trait>();
            traits.Add(new Trait("Autodidata", "Facilidade em pesquisar rapidamente, entender e usar tecnologias de software, ferramentas e linguagens desconhecidas."));
            traits.Add(new Trait("Pragmático", "Capaz de realizar juízo de valor sobre o que é realmente importante, valorizando resultados práticos e o trabalho feito."));
            traits.Add(new Trait("Humilde", "Admite os erros, sem medo de admitir que não sabe algo."));
            traits.Add(new Trait("Inquisitivo", "Se autoeduca ativamente, lê e pesquisa, buscando aprender de outros, acreditando que sempre há mais para aprender."));
            traits.Add(new Trait("Solucionador", "Sabe como atacar um problema e tem a técnica para resolver até mesmo os problemas mais difíceis, usando ferramentas adequadas"));
            traits.Add(new Trait("Pesquisador", "Consegue extrair informações, buscando documentações, na web, lendo guias, notas, fóruns de discussão, etc.<br><br>Sabe encontrar respostas."));
            model.Traits = traits;

            return PartialView(model);
        }
        #endregion

    }
}