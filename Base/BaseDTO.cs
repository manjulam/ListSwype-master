using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ListSwype.Base
{
    /// <summary>
    /// Identifies which state a DTO is currently in.
    /// </summary>
    public enum ModificationStates
    {
        /// <summary>
        /// Indicates that the DTO is not currently modified, which should be the default state.
        /// </summary>
        Unmodified,

        /// <summary>
        /// Indicates that the DTO has been modified.
        /// </summary>
        Modified,

        /// <summary>
        /// Indicates that the DTO has been added, which typically indicates that it's a new object.
        /// </summary>
        Added,

        /// <summary>
        /// Indicates that the DTO has been deleted.
        /// </summary>
        Deleted
    }

    /// <summary>
    /// Defines the base class behavior for DTO classes
    /// </summary>
    [DataContract]
    public abstract partial class BaseDTO : INotifyPropertyChanged
    {
        // Disable the warning for not being CLS compliant.
#pragma warning disable 3008

        /// <summary>
        /// Tracks the names of the fields that have been modified.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Dictionary<string, string> _modifiedFields;

        /// <summary>
        /// Tracks whether the DTO property values have changed.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool _isModified;

        /// <summary>
        ///	Specifies whether validation is enabled.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected bool _validationEnabled;

#pragma warning restore

        /// <summary>
        ///	Default class constructor.
        /// </summary>
        protected BaseDTO()
        {
            // Set the initial default state.
            ModificationState = ModificationStates.Unmodified;

            _isModified = false;
        }

        /// <summary>
        ///	Gets or sets whether the property values on this DTO have been modified or not.
        ///	We don't make this property a [DataMember] attribute because we don't want it passed
        ///	over the wire as its intended to be used locally only (e.g. either at the UI or server).
        /// </summary>
        public virtual bool IsModified
        {
            get { return _isModified; }
            set
            {
                _isModified = value;

                if (value && (_modifiedFields == null))
                {
                    _modifiedFields = new Dictionary<string, string>();
                }

                if ((_modifiedFields != null) && !value)
                {
                    // Since we're saying that the DTO has not been modified, clear out the list of modified fields.
                    _modifiedFields.Clear();
                }
            }
        }

        /// <summary>
        ///	Called by BaseDTO-derived objects to clear (set to false) the <see cref="IsModified"/> flags for all their "contained" DTOs (if any).
        /// </summary>
        public virtual void ResetIsModifiedFlag()
        {
            IsModified = false;
        }

        /// <summary>
        /// Gets a list of the modified property names.
        /// </summary>
        /// <returns>Returns a string array containing names of properties that have been modified.</returns>
        public string[] GetModifiedFields()
        {
            if (null == _modifiedFields)
            {
                return null;
            }

            return _modifiedFields.Values.ToArray();
        }

        /// <summary>
        ///	Tracks whether this object is modified, added, or deleted.  It provides more granular information than the IsModified property, 
        ///	but serves a slightly different purpose while running on the client.  IsModified will be set to true anytime a property value 
        ///	changes, but it doesn't know if an object was changed before or after it was added.  We don't want to lose track of the fact that
        ///	an object was added just because it was also modified, which overwrote the Added state.
        /// </summary>
        [DataMember]
        public ModificationStates ModificationState
        {
            get;
            set;
        }

        /// <summary>
        ///	Fires the PropertyChanged event for the INotifyPropertyChanged interface.
        /// </summary>
        /// <param name="propertyName">
        ///	Contains the name of the property that changed.
        ///	</param>
        ///	<param name="setIsModifiedFlag">
        ///	Specifies whether to set <see cref="IsModified"/> property to true (if true) or leave it unchanged (if false).
        ///	</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected virtual void FirePropertyChanged(string propertyName, bool setIsModifiedFlag)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            if (setIsModifiedFlag)
            {
                IsModified = true;

                if ((_modifiedFields != null) && (!_modifiedFields.ContainsKey(propertyName)))
                {
                    // Add the name of the modified property to the list of fields that have been modified.
                    _modifiedFields.Add(propertyName, propertyName);
                }
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///	Fires the PropertyChanged event for the INotifyPropertyChanged interface.
        /// </summary>
        /// <param name="propertyName">
        ///	Contains the name of the property that changed.
        ///	</param>
        protected virtual void FirePropertyChanged(string propertyName)
        {
            FirePropertyChanged(propertyName, true);
        }

        /// <summary>
        ///	Utility method to enumerate the public properties on the DTO and copy the corresponding property
        ///	values from the provided entity object.
        /// </summary>
        /// <param name="sourceEntity">
        ///	Contains the source entity object to copy from.
        ///	</param>
        public void CopyFromModelEntity(object sourceEntity)
        {
            CopyFromModelEntity(sourceEntity, null);
        }

        /// <summary>
        ///	Utility method to enumerate the public properties on the DTO and copy the corresponding property
        ///	values from the provided entity object.
        /// </summary>
        /// <param name="sourceEntity">
        ///		Contains the source entity object to copy from.
        ///	</param>
        ///	<param name="propertyNamesToExclude">
        ///		List of property names to exclude from copy operations.  Ignored if null or empty.
        ///	</param>
        public void CopyFromModelEntity(object sourceEntity, string[] propertyNamesToExclude)
        {
            if (null == sourceEntity)
            {
                throw new ArgumentNullException("sourceEntity");
            }

            CopyObjectProperties(sourceEntity, this, propertyNamesToExclude);
        }

        /// <summary>
        ///	Utility method to enumerate the public properties on the specified object and copy the corresponding property values from the source object.
        ///	Makes a "shallow copy", i.e. if any of the copied properties are classes, then class REFERENCES will be copied.  
        /// </summary>
        /// <param name="source">
        /// Contains the source object from which to copy the data.
        ///	</param>
        /// <param name="destination">
        ///	Contains the destination object to which to copy the data.
        ///	</param>
        ///	<param name="propertyNamesToExclude">
        ///	List of property names to exclude from copy operations.  Ignored if null or empty.
        ///	</param>
        public static void CopyObjectProperties(object source, object destination, string[] propertyNamesToExclude)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }

            // Loop through the list of public properties on this DTO object, looking for a matching property in the source object.
            foreach (PropertyInfo dtoProperty in destination.GetType().GetProperties())
            {
                // See if this one is to be excluded.
                if (dtoProperty.CanWrite && (propertyNamesToExclude == null || propertyNamesToExclude.Length == 0 || !propertyNamesToExclude.Contains<string>(dtoProperty.Name)))
                {
                    // Try to find the corresponding property on the source entity object.
                    PropertyInfo sourceProperty = source.GetType().GetProperty(dtoProperty.Name);
                    if (sourceProperty != null)
                    {
                        // Get the source value.
                        object srcValue = sourceProperty.GetValue(source, null);

                        // Copy the property value.
                        dtoProperty.SetValue(destination, srcValue, null);
                    }
                }
            }
        }

        /// <summary>
        ///	Returns a string containing the [Description] attribute value (if found) or the enum's name.
        /// </summary>
        /// <param name="value">
        ///	The enum value for which to get the description.
        ///	</param>
        /// <param name="type">
        ///	The expected type of enum which value is specified as <paramref name="value"/> parameter.
        ///	</param>
        /// <returns>
        ///	The description attribute value or the enum's name if no description is provided.
        /// </returns>
        public static string GetEnumValueDescription(Type type, object value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            string result = Enum.GetName(type, value);

            // Get the member on the type that corresponds to the value passed in
            FieldInfo fieldInfo = type.GetField(result);

            // Get the attribute on the field
            object[] attributeArray = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributeArray.Length > 0)
            {
                Debug.Assert(attributeArray.Length == 1, "Not expecting more than one [Description] attribute.");
                DescriptionAttribute attribute = (DescriptionAttribute)attributeArray[0];
                if (attribute != null)
                {
                    result = attribute.Description;
                }
            }

            return result;
        }

        /// <summary>
        ///	Returns a string containing the [Description] attribute value (if any) specified for a string constant.
        /// </summary>
        /// <param name="type">
        ///	The type of the class that declared the string constant.
        /// </param>
        /// <param name="value">
        ///	The string const value for which to get the description.
        ///	</param>
        /// <returns>
        ///	The description attribute value, if any, or an empty string if no description was found.
        /// </returns>
        /// <example>
        ///		If we have a string constant defined as follows:
        ///		<code>
        ///		class MyClass
        ///		{
        ///			[Description ("Value 'A'")]
        ///			const string MyStringConst = "A";
        ///		}
        ///		</code>,
        ///		then a call to GetStringConstDescription (typeof(MyClass), MyStringConst) will return "Value 'A'"
        /// </example>
        public static string GetStringConstDescription(Type type, string value)
        {
            if (value == null)
            {
                return String.Empty;
            }

            // Get the attribute value, if any.
            object[] attributeArray = type.GetField(value).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributeArray.Length == 0)
            {
                return String.Empty;
            }

            Debug.Assert(attributeArray.Length == 1, "Not expecting more than one [Description] attribute.");
            DescriptionAttribute attribute = (DescriptionAttribute)attributeArray[0];
            if (attribute == null)
            {
                return String.Empty;
            }

            // Found it
            return attribute.Description;
        }

        // Since Silverlight doesn't implement Enum.GetValues and Enum.GetNames, let's do it for them:
#if SILVERLIGHT
        /// <summary>
        ///	Returns an array of enum names.  (Same as <see cref="Enum.GetNames"/> method in the regular (non-Silverlight) .NET Framework.)
        /// </summary>
        /// <typeparam name="T">
        ///	Type of the enum.
        ///	</typeparam>
        /// <returns>
        ///	All field names for the specified enum type.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string[] GetEnumNames<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }
            return (from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                    where field.IsLiteral
                    select field.Name).ToArray();
        }

        /// <summary>
        ///	Returns an array of enum values.  (Same as <see cref="Enum.GetValues"/> method in the regular (non-Silverlight) .NET Framework.)
        /// </summary>
        /// <typeparam name="T">
        ///	Type of the enum.
        ///	</typeparam>
        /// <returns>
        ///	All field values for the specified enum type.
        /// </returns>
        public static T[] GetEnumValues<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }
            return (from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                    where field.IsLiteral
                    select (T)field.GetValue(type)).ToArray();
        }
#endif

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
