using FeatureFlags.Lite.Core.Evaluation;
using FeatureFlags.Lite.Core.Models;

namespace FeatureFlags.Lite.Core.Tests
{
    public class DefaultFeatureEvaluatorTests
    {
        [Fact]
        public void TestFeatureEnabled()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, FeatureContext.Empty);

            Assert.True(result);
        }

        [Fact]
        public void TestFeatureDisabled()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = false
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, FeatureContext.Empty);

            Assert.False(result);
        }

        [Fact]
        public void TestFeatureWithAllowedUser()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { UserId = "user1" });

            Assert.True(result);
        }

        [Fact]
        public void TestFeatureWithAllowedRole()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
                AllowedRoles = new List<string> { "admin" }
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { Roles = new List<string> { "admin" } });

            Assert.True(result);
        }

        [Fact]
        public void TestFeatureWithDisallowedRole()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
                AllowedRoles = new List<string> { "admin" }
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { Roles = new List<string> { "user" } });

            Assert.False(result);
        }

        [Fact]
        public void TestFeatureWithEnvironment()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
                EnvironmentOverrides = new Dictionary<string, bool>
                {
                    { "Development", true },
                    { "Production", false }
                }
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { Environment = "Development" });

            Assert.True(result);
        }

        [Fact]
        public void TestFeatureWithEnvironmentOverride()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
                EnvironmentOverrides = new Dictionary<string, bool>
                {
                    { "Development", true },
                    { "Production", false }
                }
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { Environment = "Production" });

            Assert.False(result);
        }

        [Fact]
        public void TestFeatureWithRolloutPercentageEnabled100()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
                RolloutPercentage = 100
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { UserId = "user1" });

            Assert.True(result);
        }

        [Fact]
        public void TestFeatureWithRolloutPercentageEnabled0()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = true,
                RolloutPercentage = 0
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { UserId = "user1" });

            Assert.False(result);
        }

        [Fact]
        public void TestFeatureWithRolloutPercentageDisabled100()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = false,
                RolloutPercentage = 100
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { UserId = "user1" });

            Assert.False(result);
        }

        [Fact]
        public void TestFeatureWithRolloutPercentageDisabled0()
        {
            var featureFlag = new FeatureFlag
            {
                Name = "TestFeature",
                Enabled = false,
                RolloutPercentage = 0
            };

            var evaluator = new DefaultFeatureEvaluator();

            var result = evaluator.Evaluate(featureFlag, new FeatureContext { UserId = "user1" });

            Assert.False(result);
        }
    }
}