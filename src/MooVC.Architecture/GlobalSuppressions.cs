// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Style", 
    "IDE0041:Use 'is null' check", 
    Justification = "ReferenceEquals is needed to avoid infinite loop.", 
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Ddd.Entity`1.EqualOperator(MooVC.Architecture.Ddd.Entity`1,MooVC.Architecture.Ddd.Entity`1)~System.Boolean")]

[assembly: SuppressMessage(
    "Style", 
    "IDE0046:Convert to conditional expression", 
    Justification = "The suggested modification is less readable.",
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Ddd.Entity`1.EqualOperator(MooVC.Architecture.Ddd.Entity`1,MooVC.Architecture.Ddd.Entity`1)~System.Boolean")]

[assembly: SuppressMessage(
    "Style", 
    "IDE0041:Use 'is null' check", 
    Justification = "ReferenceEquals is needed to avoid infinite loop.",
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Ddd.Value.Equals(System.Object)~System.Boolean")]

[assembly: SuppressMessage(
    "Style", 
    "IDE0041:Use 'is null' check",
    Justification = "ReferenceEquals is needed to avoid infinite loop.",
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Ddd.Value.EqualOperator(MooVC.Architecture.Ddd.Value,MooVC.Architecture.Ddd.Value)~System.Boolean")]

[assembly: SuppressMessage(
    "Style",
    "IDE0046:Convert to conditional expression",
    Justification = "The suggested modification is less readable.",
    Scope = "member",
    Target = "~M:MooVC.Architecture.Value.EqualOperator(MooVC.Architecture.Value,MooVC.Architecture.Value)~System.Boolean")]

[assembly: SuppressMessage(
    "Style", 
    "IDE0046:Convert to conditional expression",
    Justification = "The suggested modification is less readable.",
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Ddd.Value.EqualOperator(MooVC.Architecture.Ddd.Value,MooVC.Architecture.Ddd.Value)~System.Boolean")]