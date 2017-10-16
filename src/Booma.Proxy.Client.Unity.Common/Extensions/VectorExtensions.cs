using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	public static class VectorExtensions
	{
		/// <summary>
		/// Creates a new <see cref="Vector3"/> with the original <see cref="Vector2{T}"/>'s
		/// X and Y components mapped to the new <see cref="Vector3"/>'s X and Y.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>A Unity3D <see cref="Vector3"/> represetnation of the <see cref="Vector2{T}"/>.</returns>
		public static Vector3 ToUnityVector3(this Vector2<float> vector)
		{
			return new Vector3(vector.X, vector.Y);
		}

		/// <summary>
		/// Creates a new <see cref="Vector2"/> with the original <see cref="Vector2{T}"/>'s
		/// X and Y components mapped to the new <see cref="Vector2"/>'s X and Y.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>A Unity3D <see cref="Vector2"/> represetnation of the <see cref="Vector2{T}"/>.</returns>
		public static Vector2 ToUnityVector2(this Vector2<float> vector)
		{
			return new Vector2(vector.X, vector.Y);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3"/> with the original <see cref="Vector3{T}"/>'s
		/// X, Y and Z components mapped to the new <see cref="Vector3"/>'s X, Y and Z.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>A Unity3D <see cref="Vector3"/> represetnation of the <see cref="Vector3{T}"/>.</returns>
		public static Vector3 ToUnityVector3(this Vector3<float> vector)
		{
			return new Vector3(vector.X, vector.Y, vector.Z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3{T}"/> with the original <see cref="Vector3"/>'s
		/// X, Y and Z components mapped to the new <see cref="Vector3{T}"/>'s X, Y and Z.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <returns>A network <see cref="Vector3{T}"/> represetnation of the <see cref="Vector3"/>.</returns>
		public static Vector3<float> ToNetworkVector3(this Vector3 vector)
		{
			return new Vector3<float>(vector.x, vector.y, vector.z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3"/> with the original <see cref="Vector2{T}"/>'s
		/// X and Y components mapped to the new <see cref="Vector3"/>'s X and Z.
		/// With an optional Y value parameter to initialize to the Y.
		/// </summary>
		/// <param name="vector">The vector to convert.</param>
		/// <param name="optionalY">Optional Y value to initialize for the Y component of the Unity vector.</param>
		/// <returns>A Unity3D <see cref="Vector3"/> represetnation of the <see cref="Vector2{T}"/>.</returns>
		public static Vector3 ToUnityVector3XZ(this Vector2<float> vector, float optionalY = 0.0f)
		{
			return new Vector3(vector.X, optionalY, vector.Y);
		}
	}
}
