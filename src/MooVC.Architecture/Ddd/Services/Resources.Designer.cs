﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MooVC.Architecture.Ddd.Services {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MooVC.Architecture.Ddd.Services.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to The aggregate to which the event relates must be provided..
        /// </summary>
        internal static string AggregateEventArgsAggregateRequired {
            get {
                return ResourceManager.GetString("AggregateEventArgsAggregateRequired", resourceCulture);
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
        ///   Looks up a localized string similar to Version {2} of aggregate type {1} with an ID of {0:p} could not be found..
        /// </summary>
        internal static string AggregateVersionNotFoundExceptionMessage {
            get {
                return ResourceManager.GetString("AggregateVersionNotFoundExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The atomic unit must only contain changes relating to a single version increment of a single aggregate..
        /// </summary>
        internal static string AtomicUnitDistinctAggregateVersionRequired {
            get {
                return ResourceManager.GetString("AtomicUnitDistinctAggregateVersionRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The atomic unit must only contain changes triggered in the context of a single operation..
        /// </summary>
        internal static string AtomicUnitDistinctContextRequired {
            get {
                return ResourceManager.GetString("AtomicUnitDistinctContextRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The atomic unit must contain one or more domain events..
        /// </summary>
        internal static string AtomicUnitEventsRequired {
            get {
                return ResourceManager.GetString("AtomicUnitEventsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The repository must be provided..
        /// </summary>
        internal static string CoordinatedGenerateHandlerRepositoryRequired {
            get {
                return ResourceManager.GetString("CoordinatedGenerateHandlerRepositoryRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The repository must be provided..
        /// </summary>
        internal static string CoordinatedOperationHandlerRepositoryRequired {
            get {
                return ResourceManager.GetString("CoordinatedOperationHandlerRepositoryRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The bus to which the events are to be propagated must be provided..
        /// </summary>
        internal static string DomainEventPropagatorBusRequired {
            get {
                return ResourceManager.GetString("DomainEventPropagatorBusRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The reconciler from which the events are to be propagated must be provided..
        /// </summary>
        internal static string DomainEventPropagatorReconcilerRequired {
            get {
                return ResourceManager.GetString("DomainEventPropagatorReconcilerRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The repository from which the events are to be propagated must be provided..
        /// </summary>
        internal static string DomainEventPropagatorRepositoryRequired {
            get {
                return ResourceManager.GetString("DomainEventPropagatorRepositoryRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The handler for the unhandled events must be provided..
        /// </summary>
        internal static string DomainEventsUnhandledEventArgsHandlerRequired {
            get {
                return ResourceManager.GetString("DomainEventsUnhandledEventArgsHandlerRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A failure has prevented the succesful publication of atomic unit {0:p}..
        /// </summary>
        internal static string PersistentBusPublishFailure {
            get {
                return ResourceManager.GetString("PersistentBusPublishFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The storage that support persistence of domain events published via the bus must be provided..
        /// </summary>
        internal static string PersistentBusStoreRequired {
            get {
                return ResourceManager.GetString("PersistentBusStoreRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A failure has prevented the successful reprocessing of unhandled domain events..
        /// </summary>
        internal static string UnhandledDomainEventManagerProcessFailure {
            get {
                return ResourceManager.GetString("UnhandledDomainEventManagerProcessFailure", resourceCulture);
            }
        }
    }
}
