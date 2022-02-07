using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.Model;
using Assets.Scripts.CardSystem.Model.CardCommand;
using NUnit.Framework;

namespace Assets.Tests
{
    public class CardTest
    {
        [TestCase]
        public void PlayEmptyCard()
        {
            var card = new Card();

            card.Play();
        }

        [TestCase("Goblin")]
        [TestCase("This is a very very very  very very very very very very very very long name, we should do something about this")]
        [TestCase(null)]
        [TestCase("")]

        public void PlayACardWithAName(string cardName)
        {
            var card = new Card()
            {
                Name = cardName
            };

            card.Play();
        }

        [Test]
        public void PlayCardWithBasicCommand()
        {
            var card = new Card()
            {
                Name = "Card With Basic Command",
                Commands = new List<ICardCommand>()
                {
                    new EmptyCardCommand()
                }
            };

            card.Play();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void PlayCardWithNumberCommand(int value)
        {
            var card = new Card()
            {
                Name = "Card With Number Command",
                Commands = new List<ICardCommand>()
                {
                    new NumberCommand(value)
                }
            };

            card.Play();

        }

        [TestCase]
        public void PlayCardWithMultipleCommandsAndGetReports()
        {
            var card = new Card()
            {
                Name = "Card With Multiple Commands Returning Reports",
                Commands = new List<ICardCommand>()
                {
                    new EmptyCardCommand(),
                    new EmptyCardCommand(),
                    new EmptyCardCommand(),
                }
            };

            var report = card.Play();
            Assert.AreEqual(card.Commands.Count, report.CardCommandReports.Count);

        }

        [TestCase(0, 0, 0)]
        public void PlayCardwithNumberCommandAndGetSuccessfulReports(int v1, int v2, int v3)
        {
            var card = new Card()
            {
                Name = "Multiple Values Card",
                Commands = new List<ICardCommand>()
                {
                    new NumberCommand(v1),
                    new NumberCommand(v2),
                    new NumberCommand(v3)
                }
            };

            var report = card.Play();

            Assert.IsTrue(report.CardCommandReports.Count == 3);
            Assert.IsTrue(report.CardCommandReports.All(rep => rep.CardCommandStatus == CardCommandStatus.Success));

        }

        [TestCase(1,1,CardCommandStatus.Success)]
        [TestCase(1, 0, CardCommandStatus.Failed)]
        public void PlayCardWithComparingCommand(int v1, int v2, CardCommandStatus expectedResult)
        {
            var card = new Card()
            {
                Name = "Failed Report Card",
                Commands = new List<ICardCommand>()
                {
                    new ComparingCommand(v1, v2),
                }
            };

            var report = card.Play().CardCommandReports[0];

            Assert.AreEqual(expectedResult, report.CardCommandStatus);
        }

    }
}