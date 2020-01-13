namespace Zz.Core.Data.Entity.Metadata
{
    public abstract class ITypeModel : IMetadata
    {
        /// <summary>
        /// TypeName
        /// </summary>
        public virtual string AssemblyQualifiedName { get; set; }
    }
}
