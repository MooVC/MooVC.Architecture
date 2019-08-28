﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MooVC.Architecture {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MooVC.Architecture.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to A conflict has been detected while attempting to commit changes to aggregate of type {1} with an ID of {0:p}.  Version {2} was received, but version {3} has been previously committed..
        /// </summary>
        internal static string AggregateConflictDetectedExceptionExistingEntryMessage {
            get {
                return ResourceManager.GetString("AggregateConflictDetectedExceptionExistingEntryMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A conflict has been detected while attempting to commit changes to aggregate of type {1} with an ID of {0:p}.  Version {2} was received, but the initial version was expected..
        /// </summary>
        internal static string AggregateConflictDetectedExceptionNoExistingEntryMessage {
            get {
                return ResourceManager.GetString("AggregateConflictDetectedExceptionNoExistingEntryMessage", resourceCulture);
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
        ///   Looks up a localized string similar to Event {2:p} of type {3} does not apply to aggregate {1:p} of type {0}..
        /// </summary>
        internal static string AggregateEventMismatchExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateEventMismatchExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is currently no aggregate of type {1} with an ID of {0:p}..
        /// </summary>
        internal static string AggregateNotFoundExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateNotFoundExceptionMessage", resourceCulture);
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
        ///   Looks up a localized string similar to Version {2} of aggregate type {1} with an ID of {0:p} could not be found..
        /// </summary>
        internal static string AggregateVersionNotFoundExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateVersionNotFoundExceptionMessage", resourceCulture);
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
        ///   Looks up a localized string similar to A non-empty reference of aggregate type {0} is required..
        /// </summary>
        internal static string EnsureReferenceIsNotEmptyMessage {
            get {
                return ResourceManager.GetString("EnsureReferenceIsNotEmptyMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-version specific reference of aggregate type {0} is required..
        /// </summary>
        internal static string EnsureReferenceIsNotVersionSpecificMessage {
            get {
                return ResourceManager.GetString("EnsureReferenceIsNotVersionSpecificMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A version specific reference of aggregate type {0} is required..
        /// </summary>
        internal static string EnsureReferenceIsVersionSpecificMessage {
            get {
                return ResourceManager.GetString("EnsureReferenceIsVersionSpecificMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum supported ID of {0} for entity of type {1} has been exceeded..
        /// </summary>
        internal static string EntityMaximumIdValueExceededExceptionMessage {
            get {
                return ResourceManager.GetString("EntityMaximumIdValueExceededExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type {2} with an ID of {0:p} and a version of {1} is not in a suitable state for a load from history..
        /// </summary>
        internal static string EventCentricAggregateInvalidStateForLoadFromHistory {
            get {
                return ResourceManager.GetString("EventCentricAggregateInvalidStateForLoadFromHistory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Handler for event of type {0} is not supported by aggregate of type {1}..
        /// </summary>
        internal static string EventCentricAggregateRootDomainEventHandlerNotSupportedException {
            get {
                return ResourceManager.GetString("EventCentricAggregateRootDomainEventHandlerNotSupportedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context for this request must be provided..
        /// </summary>
        internal static string GenericContextRequired {
            get {
                return ResourceManager.GetString("GenericContextRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Handler for message of type {0} has failed to process message {1:p} for transaction {2:p}..
        /// </summary>
        internal static string HandlerFailureExceptionMessage {
            get {
                return ResourceManager.GetString("HandlerFailureExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must provide the paging instructions for this query..
        /// </summary>
        internal static string PaginatedQueryPagingRequired {
            get {
                return ResourceManager.GetString("PaginatedQueryPagingRequired", resourceCulture);
            }
        }
    }
}
