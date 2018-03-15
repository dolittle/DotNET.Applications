/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications
{
    /// <summary>
    /// Extends <see cref="IApplicationStructureMapBuilder"/> with more concrete concepts
    /// </summary>
    public static class ApplicationStructureMapBuilderExtensions
    {
        /// <summary>
        /// Includes a convention format for the Domain aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureMapBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureMapBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureMapBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureMapBuilder Domain(this IApplicationStructureMapBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Domain, format);
        }

        /// <summary>
        /// Includes a convention format for the Events aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureMapBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureMapBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureMapBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureMapBuilder Events(this IApplicationStructureMapBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Events, format);
        }

        /// <summary>
        /// Includes a convention format for the Read aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureMapBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureMapBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureMapBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureMapBuilder Read(this IApplicationStructureMapBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Read, format);
        }

        /// <summary>
        /// Includes a convention format for the Frontend aspect of the application
        /// </summary>
        /// <param name="builder"><see cref="IApplicationStructureMapBuilder">Builder</see> to build on</param>
        /// <param name="format">Convention string format</param>
        /// <returns><see cref="IApplicationStructureMapBuilder">Builder</see> to continue building on</returns>
        /// <remarks>
        /// <seealso cref="IApplicationStructureMapBuilder.Include(ApplicationArea, string)">for more details on format</seealso>
        /// </remarks>
        public static IApplicationStructureMapBuilder Frontend(this IApplicationStructureMapBuilder builder, string format)
        {
            return builder.Include(ApplicationAreas.Frontend, format);
        }
    }
}
