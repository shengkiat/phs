
namespace PHS.DB.Attributes
{
    using System;

    /// <summary>
    ///     Allows skipping of tracking of columns.
    ///     Place this attributer on the entity property which you dont wish to track for audit.
    /// </summary>

    public class SkipTrackingAttribute : Attribute
    {
    }
}
