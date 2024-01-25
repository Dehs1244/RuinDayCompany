using RuinDayCompany.Core;
using RuinDayCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Tests.Shuffle
{
    public class ShuffleTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(10)]
        public void ShuffleTest(int crewCount)
        {
            ICrewShuffler shuffler = new CrewShuffler(_GetFakeCrew(crewCount));
            var infested = shuffler.Shuffle();

            infested.Should().HaveCount(crewCount);
            if (crewCount < shuffler.MinPlayers)
            {
                infested.Impostors.Should().HaveCount(0);
                Assert.Throws<InvalidOperationException>(() => infested.MainImpostor);
            }
            else
            {
                infested.Impostors.Should().HaveCountGreaterThan(0);
                infested.MainImpostor.Name.Should().NotEndWith(shuffler.MinPlayers.ToString());
            }
        }

        private ICollection<IRuinCrewmate> _GetFakeCrew(int count)
        {
            Collection<IRuinCrewmate> crew = new Collection<IRuinCrewmate>();

            for(var i = 0; i < count; i++)
            {
                var crewmate = new Mock<IRuinCrewmate>();
                crewmate.SetupGet(x => x.Name).Returns($"Test Player {i + 1}");
                crew.Add(crewmate.Object);
            }

            return crew;
        }
    }
}
