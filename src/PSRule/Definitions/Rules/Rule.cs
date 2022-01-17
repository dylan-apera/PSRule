// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Newtonsoft.Json;
using PSRule.Definitions.Expressions;
using PSRule.Pipeline;
using YamlDotNet.Serialization;

namespace PSRule.Definitions.Rules
{
    public interface IRuleV1 : IResource, IDependencyTarget
    {
        [Obsolete("Use Name instead.")]
        string RuleName { get; }

        string Synopsis { get; }

        [Obsolete("Use Synopsis instead.")]
        string Description { get; }

        ResourceTags Tag { get; }

        SourceFile Source { get; }
    }

    internal interface IRuleSpec
    {
        LanguageIf Condition { get; }

        string[] Type { get; }

        string[] With { get; }
    }

    [Spec(Specs.V1, Specs.Rule)]
    internal sealed class RuleV1 : InternalResource<RuleV1Spec>, IResource, IRuleV1
    {
        public RuleV1(string apiVersion, SourceFile source, ResourceMetadata metadata, ResourceHelpInfo info, RuleV1Spec spec)
            : base(ResourceKind.Rule, apiVersion, source, metadata, info, spec)
        {
            Ref = ResourceHelper.GetIdNullable(source.ModuleName, metadata.Ref, ResourceIdKind.Ref);
            Alias = ResourceHelper.GetRuleId(source.ModuleName, metadata.Alias, ResourceIdKind.Alias);
        }

        [JsonIgnore]
        [YamlIgnore]
        public ResourceId? Ref { get; }

        [JsonIgnore]
        [YamlIgnore]
        public ResourceId[] Alias { get; }

        /// <summary>
        /// A human readable block of text, used to identify the purpose of the rule.
        /// </summary>
        [JsonIgnore]
        [YamlIgnore]
        public string Synopsis => Info.Synopsis;

        ResourceId? IDependencyTarget.Ref => Ref;

        ResourceId[] IDependencyTarget.Alias => Alias;

        // Not supported with resource rules.
        ResourceId[] IDependencyTarget.DependsOn => Array.Empty<ResourceId>();

        bool IDependencyTarget.Dependency => Source.IsDependency();

        ResourceId? IResource.Ref => Ref;

        ResourceId[] IResource.Alias => Alias;

        string IRuleV1.RuleName => Name;

        ResourceTags IRuleV1.Tag => Metadata.Tags;

        SourceFile IRuleV1.Source => Source;

        string IRuleV1.Description => Info.Synopsis;
    }

    internal sealed class RuleV1Spec : Spec, IRuleSpec
    {
        public LanguageIf Condition { get; set; }

        /// <summary>
        /// An optional type precondition before the rule is evaluated.
        /// </summary>
        public string[] Type { get; set; }

        /// <summary>
        /// An optional selector precondition before the rule is evaluated.
        /// </summary>
        public string[] With { get; set; }
    }
}