﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MooVC.Architecture.Ddd {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MooVC.Architecture.Ddd.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context in which the aggregate was requested must be provided..
        /// </summary>
        internal static string AggregateDoesNotExistExceptionContextRequired {
            get {
                return ResourceManager.GetString("AggregateDoesNotExistExceptionContextRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An empty reference for aggregate type {0} cannot be retrieved..
        /// </summary>
        internal static string AggregateDoesNotExistExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateDoesNotExistExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The aggregate from which the request originated must be provided..
        /// </summary>
        internal static string AggregateEventMismatchExceptionAggregateRequired {
            get {
                return ResourceManager.GetString("AggregateEventMismatchExceptionAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The aggregate that mismatches with the aggregate from which the request originated must be provided..
        /// </summary>
        internal static string AggregateEventMismatchExceptionEventAggregateRequired {
            get {
                return ResourceManager.GetString("AggregateEventMismatchExceptionEventAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event {3:p}, version {5} of type {4} does not apply to aggregate {0:p}, version {2} of type {1}..
        /// </summary>
        internal static string AggregateEventMismatchExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateEventMismatchExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The aggregate from which the request originated must be provided..
        /// </summary>
        internal static string AggregateEventSequenceUnorderedExceptionAggregateRequired {
            get {
                return ResourceManager.GetString("AggregateEventSequenceUnorderedExceptionAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The events that are intended to be applied to the aggregate must be provided..
        /// </summary>
        internal static string AggregateEventSequenceUnorderedExceptionEventsRequired {
            get {
                return ResourceManager.GetString("AggregateEventSequenceUnorderedExceptionEventsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type {2} with an ID of {0:p} and a version of {1} cannot accept the proposed state from history as the sequence is unordered..
        /// </summary>
        internal static string AggregateEventSequenceUnorderedExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateEventSequenceUnorderedExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type {2} with an ID of {0:p} and a version of {1} cannot accept its state from history as it current possesses uncommitted changes..
        /// </summary>
        internal static string AggregateHasUncommittedChangesExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateHasUncommittedChangesExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type {2} with an ID of {0:p} and a version of {1} cannot accept the proposed sequence as it starts from version {3}..
        /// </summary>
        internal static string AggregateHistoryInvalidForStateExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateHistoryInvalidForStateExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request {0} could not be fulfilled due to a failure to satisfy the following invariants as defined by aggregate {1}:
        ///
        ///{2}.
        /// </summary>
        internal static string AggregateInvariantsNotSatisfiedDomainExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateInvariantsNotSatisfiedDomainExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reference for aggregate {0:p} of type {1} cannot be converted type {2}..
        /// </summary>
        internal static string AggregateReferenceMismatchExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateReferenceMismatchExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The reference that serves as the subject of the conversion must be provided..
        /// </summary>
        internal static string AggregateReferenceMismatchExceptionReferenceRequired {
            get {
                return ResourceManager.GetString("AggregateReferenceMismatchExceptionReferenceRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The destination must be provided..
        /// </summary>
        internal static string AggregateRootExtensionsSaveDestinationRequired {
            get {
                return ResourceManager.GetString("AggregateRootExtensionsSaveDestinationRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The aggregate must be provided..
        /// </summary>
        internal static string AggregateRootExtensionsToReferenceAggregateRequired {
            get {
                return ResourceManager.GetString("AggregateRootExtensionsToReferenceAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty ID for the aggregate must be provided..
        /// </summary>
        internal static string AggregateRootIdRequired {
            get {
                return ResourceManager.GetString("AggregateRootIdRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A reference to the aggregate from which the domain event was raised must be provided..
        /// </summary>
        internal static string DomainEventAggregateReferenceRequired {
            get {
                return ResourceManager.GetString("DomainEventAggregateReferenceRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A versioned reference for the aggregate must be provided..
        /// </summary>
        internal static string DomainEventAggregateRequired {
            get {
                return ResourceManager.GetString("DomainEventAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context in which the event was raised must be provided..
        /// </summary>
        internal static string DomainEventContextRequired {
            get {
                return ResourceManager.GetString("DomainEventContextRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty reference of aggregate type {0} is required..
        /// </summary>
        internal static string EnsureReferenceIsNotEmptyMessage {
            get {
                return ResourceManager.GetString("EnsureReferenceIsNotEmptyMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The reference provided is for an aggregate of type {0}.  {1} is required..
        /// </summary>
        internal static string EnsureReferenceIsOfTypeMessage {
            get {
                return ResourceManager.GetString("EnsureReferenceIsOfTypeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Changed to the state of an aggregate of type {0} must be made through ApplyChanges..
        /// </summary>
        internal static string EventCentricAggregateRootStateChangesDenied {
            get {
                return ResourceManager.GetString("EventCentricAggregateRootStateChangesDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The aggregate of type {0} that serves as the subject of the message must be provided..
        /// </summary>
        internal static string MessageAggregateRequired {
            get {
                return ResourceManager.GetString("MessageAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A single, non-empty reference, must be provided..
        /// </summary>
        internal static string MultiTypeReferenceReferenceRequired {
            get {
                return ResourceManager.GetString("MultiTypeReferenceReferenceRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A reference to the aggregate must be provided..
        /// </summary>
        internal static string ProjectionAggregateRequired {
            get {
                return ResourceManager.GetString("ProjectionAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The aggregate to be referenced must be provided..
        /// </summary>
        internal static string ReferenceCreateAggregateRequired {
            get {
                return ResourceManager.GetString("ReferenceCreateAggregateRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type of the aggregate to be referenced must be provided..
        /// </summary>
        internal static string ReferenceCreateTypeRequired {
            get {
                return ResourceManager.GetString("ReferenceCreateTypeRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type of the reference could not be determined..
        /// </summary>
        internal static string ReferenceDeserializeTypeTypeIndeterminate {
            get {
                return ResourceManager.GetString("ReferenceDeserializeTypeTypeIndeterminate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty ID for the aggregate this reference represents must be provided..
        /// </summary>
        internal static string ReferenceIdRequired {
            get {
                return ResourceManager.GetString("ReferenceIdRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty reference is required..
        /// </summary>
        internal static string ReferenceNonEmptyRequired {
            get {
                return ResourceManager.GetString("ReferenceNonEmptyRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type of the reference must be concrete..
        /// </summary>
        internal static string ReferenceTypeRequired {
            get {
                return ResourceManager.GetString("ReferenceTypeRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The footer of previous version {0} is invalid..
        /// </summary>
        internal static string SignedVersionPreviousFooterInvalid {
            get {
                return ResourceManager.GetString("SignedVersionPreviousFooterInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The previous version must be provided..
        /// </summary>
        internal static string SignedVersionPreviousRequired {
            get {
                return ResourceManager.GetString("SignedVersionPreviousRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unversioned.
        /// </summary>
        internal static string SignedVersionUnversioned {
            get {
                return ResourceManager.GetString("SignedVersionUnversioned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty ID for the aggregate this versioned reference represents must be provided..
        /// </summary>
        internal static string VersionedReferenceIdRequired {
            get {
                return ResourceManager.GetString("VersionedReferenceIdRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The version of the aggregate must be provided..
        /// </summary>
        internal static string VersionedReferenceVersionRequired {
            get {
                return ResourceManager.GetString("VersionedReferenceVersionRequired", resourceCulture);
            }
        }
    }
}
