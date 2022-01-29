// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using PSRule.Definitions.Baselines;
using PSRule.Definitions.ModuleConfigs;
using PSRule.Definitions.Rules;
using PSRule.Definitions.Selectors;
using PSRule.Definitions.SuppressionGroups;

namespace PSRule.Definitions
{
    public abstract class Spec
    {
        private const string FullNameSeparator = "/";

        protected Spec() { }

        public static string GetFullName(string apiVersion, string name)
        {
            return string.Concat(apiVersion, FullNameSeparator, name);
        }
    }

    internal static class Specs
    {
        internal const string V1 = "github.com/microsoft/PSRule/v1";

        internal const string Rule = "Rule";
        internal const string Baseline = "Baseline";
        internal const string ModuleConfig = "ModuleConfig";
        internal const string Selector = "Selector";
        internal const string SuppressionGroup = "SuppressionGroup";

        public readonly static ISpecDescriptor[] BuiltinTypes = new ISpecDescriptor[]
        {
            new SpecDescriptor<RuleV1, RuleV1Spec>(V1, Rule),
            new SpecDescriptor<Baseline, BaselineSpec>(V1, Baseline),
            new SpecDescriptor<ModuleConfigV1, ModuleConfigV1Spec>(V1, ModuleConfig),
            new SpecDescriptor<SelectorV1, SelectorV1Spec>(V1, Selector),
            new SpecDescriptor<SuppressionGroupV1, SuppressionGroupV1Spec>(V1, SuppressionGroup)
        };
    }
}