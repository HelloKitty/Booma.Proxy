﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace FreecraftCore.Serializer
{
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
	//THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
	/// <summary>
	/// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
	/// code for the Type: <see cref = "GameSettings"/>
	/// </summary>
	public sealed partial class GameSettings_Serializer : BaseAutoGeneratedSerializerStrategy<GameSettings_Serializer, GameSettings>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(GameSettings value, Span<byte> buffer, ref int offset)
		{
			//Type: GameSettings Field: 1 Name: Difficulty Type: DifficultyType
			;
			value.Difficulty = GenericPrimitiveEnumTypeSerializerStrategy<DifficultyType, Byte>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 2 Name: isBattle Type: Boolean
			;
			value.isBattle = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 3 Name: Event Type: Byte
			;
			value.Event = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 4 Name: Section Type: SectionId
			;
			value.Section = GenericPrimitiveEnumTypeSerializerStrategy<SectionId, Byte>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 5 Name: isChallengeMode Type: Boolean
			;
			value.isChallengeMode = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 6 Name: RandomSeed Type: UInt32
			;
			value.RandomSeed = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 7 Name: Episode Type: EpisodeType
			;
			value.Episode = GenericPrimitiveEnumTypeSerializerStrategy<EpisodeType, Byte>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 8 Name: One Type: Byte
			;
			value.One = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 9 Name: isSinglePlayer Type: Boolean
			;
			value.isSinglePlayer = GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Read(buffer, ref offset);
			//Type: GameSettings Field: 10 Name: unused Type: Byte
			;
			value.unused = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(GameSettings value, Span<byte> buffer, ref int offset)
		{
			//Type: GameSettings Field: 1 Name: Difficulty Type: DifficultyType
			;
			GenericPrimitiveEnumTypeSerializerStrategy<DifficultyType, Byte>.Instance.Write(value.Difficulty, buffer, ref offset);
			//Type: GameSettings Field: 2 Name: isBattle Type: Boolean
			;
			GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.isBattle, buffer, ref offset);
			//Type: GameSettings Field: 3 Name: Event Type: Byte
			;
			BytePrimitiveSerializerStrategy.Instance.Write(value.Event, buffer, ref offset);
			//Type: GameSettings Field: 4 Name: Section Type: SectionId
			;
			GenericPrimitiveEnumTypeSerializerStrategy<SectionId, Byte>.Instance.Write(value.Section, buffer, ref offset);
			//Type: GameSettings Field: 5 Name: isChallengeMode Type: Boolean
			;
			GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.isChallengeMode, buffer, ref offset);
			//Type: GameSettings Field: 6 Name: RandomSeed Type: UInt32
			;
			GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.RandomSeed, buffer, ref offset);
			//Type: GameSettings Field: 7 Name: Episode Type: EpisodeType
			;
			GenericPrimitiveEnumTypeSerializerStrategy<EpisodeType, Byte>.Instance.Write(value.Episode, buffer, ref offset);
			//Type: GameSettings Field: 8 Name: One Type: Byte
			;
			BytePrimitiveSerializerStrategy.Instance.Write(value.One, buffer, ref offset);
			//Type: GameSettings Field: 9 Name: isSinglePlayer Type: Boolean
			;
			GenericTypePrimitiveSerializerStrategy<Boolean>.Instance.Write(value.isSinglePlayer, buffer, ref offset);
			//Type: GameSettings Field: 10 Name: unused Type: Byte
			;
			BytePrimitiveSerializerStrategy.Instance.Write(value.unused, buffer, ref offset);
		}
	}
}