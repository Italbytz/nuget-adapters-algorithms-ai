﻿using Italbytz.Adapters.Algorithms.AI.Search.Agent;
using Italbytz.Adapters.Algorithms.AI.Search.Framework;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.Problem;
using Italbytz.Adapters.Algorithms.AI.Search.Framework.QSearch;
using Italbytz.Adapters.Algorithms.AI.Search.Informed;
using Italbytz.Adapters.Algorithms.Tests.Environment.Map;
using Italbytz.Ports.Algorithms.AI.Agent;
using Italbytz.Ports.Algorithms.AI.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Italbytz.Adapters.Algorithms.AI.Tests.Unit.Search.Informed
{
    public class RecursiveBestFirstSearchTest
    {
        private const bool ConsoleLogging = false;

        private static ILoggerFactory _loggerFactory =
            NullLoggerFactory.Instance;

        [SetUp]
        public void Setup()
        {
            if (ConsoleLogging)
                _loggerFactory =
                    LoggerFactory.Create(builder => builder.AddConsole());
        }

        [TearDown]
        public void Cleanup()
        {
            _loggerFactory.Dispose();
        }


        [Test]
        public void TestSimplifiedRoadMapOfRomaniaFromArad()
        {
            var romaniaMap = new SimplifiedRoadMapOfPartOfRomania();
            var search = new RecursiveBestFirstSearch<string, MoveToAction>(MapFunctions.CreateSLDHeuristicFunction(SimplifiedRoadMapOfPartOfRomania.BUCHAREST, romaniaMap), true);
            var actions = TestSimplifiedRoadMapOfRomania(search, SimplifiedRoadMapOfPartOfRomania.ARAD);
            Assert.Multiple(() =>
            {
                Assert.That(actions,
                    Is.EqualTo(
                        "MoveToAction[name=moveTo, location=Sibiu], MoveToAction[name=moveTo, location=RimnicuVilcea], MoveToAction[name=moveTo, location=Pitesti], MoveToAction[name=moveTo, location=Bucharest]"));
                Assert.That(
                    search.Metrics.Get(QueueSearch<string, MoveToAction>
                        .MetricPathCost), Is.EqualTo("418"));
                Assert.That(
                    search.Metrics.Get(QueueSearch<string, MoveToAction>
                        .MetricNodesExpanded), Is.EqualTo("6"));
            });
        }



        private static string TestSimplifiedRoadMapOfRomania(ISearchForActions<string, MoveToAction> search, string initialState)
        {
            var romaniaMap = new SimplifiedRoadMapOfPartOfRomania();
            var problem = new GeneralProblem<string, MoveToAction>(initialState,
                MapFunctions.CreateActionsFunction(romaniaMap),
                MapFunctions.GetResult, MapFunctions.TestGoal,
                MapFunctions.CreateDistanceStepCostFunction(romaniaMap));
            var agent = new SearchAgent<IPercept, string, MoveToAction>(problem,
                search, _loggerFactory);
            var actions = agent.Actions;
            return string.Join(", ", actions);
        }
    }
}
