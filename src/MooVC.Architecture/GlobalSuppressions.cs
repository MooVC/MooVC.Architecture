// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style", 
    "IDE0041:Use 'is null' check", 
    Justification = "ReferenceEquals is needed to avoid infinite loop.", 
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Value.Equals(System.Object)~System.Boolean")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style", 
    "IDE0041:Use 'is null' check", 
    Justification = "ReferenceEquals is needed to avoid infinite loop.", 
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Value.EqualOperator(MooVC.Architecture.Value,MooVC.Architecture.Value)~System.Boolean")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style", 
    "IDE0046:Convert to conditional expression", 
    Justification = "The suggested modification is less readable.", 
    Scope = "member", 
    Target = "~M:MooVC.Architecture.Value.EqualOperator(MooVC.Architecture.Value,MooVC.Architecture.Value)~System.Boolean")]