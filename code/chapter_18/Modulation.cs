//
// SPDX-License-Identifier: CC0-1.0
//
// This example code file is released to the public under Creative Commons CC0.
// See https://creativecommons.org/publicdomain/zero/1.0/legalcode
//
// To the extent possible under law, the author has waived all copyright and
// related or neighboring rights to this example code file.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

namespace Coding4Engineers.Chapter18
{  
    /// <summary>
    /// Encapsulates a modulation of a surface
    /// The coordinates and offset are normalized (0..1)
    /// </summary>
    public interface ISurfaceModulation
    {
        /// <summary>
        /// Returns the normalized offset (0..1) at surface coordinate u/v
        /// </summary>
        /// <param name="u">Surface coordinate U 0..1, usually in the width direction of a surface</param>
        /// <param name="v">Surface coordinate V 0..1, usually in the height direction of a surface</param>
        /// <returns></returns>
        float fOffset(float u, float v);
    }

    /// <summary>
    /// Surface modulation that does nothing
    /// </summary>
    public class SurfaceModulationNoop : ISurfaceModulation
    {
        public float fOffset(float u, float v) => 0f;
    }

    /// <summary>
    /// A simple sine wave surface modulation in both U and V directions
    /// </summary>
    public class SurfaceModulationSineWaveUV : ISurfaceModulation
    {
        public SurfaceModulationSineWaveUV( int nRepeatU = 1,
                                            int nRepeatV = 1)
        {
            m_fRepeatU = nRepeatU;
            m_fRepeatV = nRepeatV;
        }
        public float fOffset(float u, float v)
        {
            // Map 0..1 to 0..2π for one complete sine wave cycle
            float fUAngle = u * 2f * float.Pi * m_fRepeatU;
            float fVAngle = v * 2f * float.Pi * m_fRepeatV;

            // Map to 0..1
            return  ((float.Sin(fUAngle) + 1f) * 0.25f) +
                    ((float.Sin(fVAngle) + 1f) * 0.25f);
        }

        float m_fRepeatU;
        float m_fRepeatV;
    }

    public class SurfaceModulationCosineWaveUV : ISurfaceModulation
    {
        public SurfaceModulationCosineWaveUV( int nRepeatU = 1,
                                            int nRepeatV = 1)
        {
            m_fRepeatU = nRepeatU;
            m_fRepeatV = nRepeatV;
        }
        public float fOffset(float u, float v)
        {
            // Map 0..1 to 0..2π for one complete sine wave cycle
            float fUAngle = u * 2f * float.Pi * m_fRepeatU;
            float fVAngle = v * 2f * float.Pi * m_fRepeatV;

            // Map to 0..1
            return  ((float.Cos(fUAngle) + 1f) * 0.25f) +
                    ((float.Cos(fVAngle) + 1f) * 0.25f);
        }

        float m_fRepeatU;
        float m_fRepeatV;
    }

    public class SurfaceModulationTriangleWaveUV : ISurfaceModulation
    {
        public SurfaceModulationTriangleWaveUV( int nRepeatU = 1,
                                            int nRepeatV = 1)
        {
            m_fRepeatU = nRepeatU;
            m_fRepeatV = nRepeatV;
        }
        public float fOffset(float u, float v)
        {
            // Map 0..1 to 0..2π for one complete sine wave cycle
            float fUAngle = u * 2f * float.Pi * m_fRepeatU;
            float fVAngle = v * 2f * float.Pi * m_fRepeatV;

            // Map to 0..1
            return  ((float.Abs(float.Sin(fUAngle)) + 1f) * 0.25f) +
                    ((float.Abs(float.Sin(fVAngle)) + 1f) * 0.25f);
        }

        float m_fRepeatU;
        float m_fRepeatV;

    }

    public class SurfaceModulationComposite : ISurfaceModulation
    {
        public SurfaceModulationComposite(ISurfaceModulation xMod1, ISurfaceModulation xMod2) => (m_xMod1, m_xMod2) = (xMod1, xMod2);
        public float fOffset(float u, float v) => m_xMod1.fOffset(u, v) + m_xMod2.fOffset(u, v);
        ISurfaceModulation m_xMod1, m_xMod2;
    }

    public class SurfaceModulationCombinedSineWaveUV : ISurfaceModulation
    {
        public SurfaceModulationCombinedSineWaveUV( int nRepeatU = 1, int nRepeatV = 1, int nRepeatU2 = 1, int nRepeatV2 = 1)
        {
            m_fRepeatU = nRepeatU;
            m_fRepeatV = nRepeatV;
            m_fRepeatU2 = nRepeatU2;
            m_fRepeatV2 = nRepeatV2;
        }
        public float fOffset(float u, float v)
        {
            float fResult = 0f;
            for (int i = 0; i < m_fRepeatU; i++)
            {
                float fUAngle = u * 2f * float.Pi * (i + 1);
                fResult += (float.Sin(fUAngle) + 1f) * 0.25f / (i + 1);
            }
            for (int i = 0; i < m_fRepeatV; i++)
            {
                float fVAngle = v * 2f * float.Pi * (i + 1);
                fResult += (float.Sin(fVAngle) + 1f) * 0.25f / (i + 1);
            }
            return fResult;
        }

        float m_fRepeatU;
        float m_fRepeatV;
        float m_fRepeatU2;
        float m_fRepeatV2;
    }

}