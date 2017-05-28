/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Strings
{
    /// <summary>
    /// Builder for <see cref="VariableStringSegment"/>
    /// </summary>
    public interface IVariableStringSegmentBuilder : ISegmentBuilder<VariableStringSegment>
    {
        /// <summary>
        /// Expect a single instance of <see cref="VariableStringSegment"/> 
        /// </summary>
        /// <returns><see cref="IVariableStringSegmentBuilder"/> to continue to build on</returns>
        IVariableStringSegmentBuilder Single();

        /// <summary>
        /// Expect to find multiple instances of this <see cref="VariableStringSegment"/>
        /// </summary>
        /// <returns><see cref="IVariableStringSegmentBuilder"/> to continue to build on</returns>
        IVariableStringSegmentBuilder Recurring();

        /// <summary>
        /// Mark <see cref="VariableStringSegment"/> as optional
        /// </summary>
        /// <returns><see cref="IVariableStringSegmentBuilder"/> to continue to build on</returns>
        IVariableStringSegmentBuilder Optional();

        /// <summary>
        /// Make the <see cref="VariableStringSegment"/> a child
        /// </summary>
        /// <returns><see cref="IVariableStringSegmentBuilder"/> to continue to build on</returns>
        IVariableStringSegmentBuilder DependingOnPrevious();
    }
}