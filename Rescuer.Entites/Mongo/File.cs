namespace Rescuer.Entites.Mongo
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// Enum FileType
        /// </summary>
        public int FileType { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Keywords (Hastag) json olarak tutarız
        /// </summary>
        public string Keywords { get; set; }
    }
}