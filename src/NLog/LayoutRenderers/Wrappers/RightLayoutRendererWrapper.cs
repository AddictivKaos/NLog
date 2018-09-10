// 
// Copyright (c) 2004-2018 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
    /// <summary>
    /// Right part of a text
    /// </summary>
    [LayoutRenderer("right")]
    [ThreadAgnostic]
    public class RightLayoutRendererWrapper : WrapperLayoutRendererBase
    {
        private readonly SubstringLayoutRendererWrapper _substringWrapper;

        /// <summary>
        /// New wrapper
        /// </summary>
        public RightLayoutRendererWrapper()
        {
            _substringWrapper = new SubstringLayoutRendererWrapper();
        }

        /// <summary>
        /// Gets or sets the length in characters. 
        /// </summary>
        /// <value>Index</value>
        /// <docgen category='Transformation Options' order='10' />
        [RequiredParameter]
        public int Length
        {
            get { return _substringWrapper.Length ?? 0; }
            set
            {
                _substringWrapper.Length = value;
                _substringWrapper.Start = -value;
            }
        }


        /// <inheritdoc />
        protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
        {
            _substringWrapper.Inner = Inner;
            _substringWrapper.DoSubstring(logEvent, builder, orgLength);
        }

        /// <inheritdoc />
        protected override string Transform(string text)
        {
            _substringWrapper.Inner = Inner;
            return _substringWrapper.DoTransform(text);
        }
    }
}