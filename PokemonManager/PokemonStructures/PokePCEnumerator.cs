﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManager.PokemonStructures {
	public class PokePCEnumerator : IEnumerator<IPokemon> {

		private IPokePC pokePC;
		private int boxIndex;
		private int pokeIndex;

		public PokePCEnumerator(IPokePC pokePC) {
			this.pokePC		= pokePC;
			this.boxIndex	= -2;
			this.pokeIndex	= -1;
		}

		public IPokemon Current {
			get {
				if (pokeIndex != -1) {
					if (pokePC.Daycare != null && boxIndex == -2)
						return pokePC.Daycare[pokeIndex];
					if (pokePC.Party != null && boxIndex == -1)
						return pokePC.Party[pokeIndex];
					else if (boxIndex < pokePC.NumBoxes)
						return pokePC[boxIndex][pokeIndex];
				}
				return null;
			}
		}
		public bool MoveNext() {
			if (boxIndex == -2) {
				pokeIndex++;
				if (pokePC.Daycare != null) {
					while (pokeIndex < pokePC.Daycare.NumSlots) {
						if (pokePC.Daycare[pokeIndex] != null)
							return true;
						pokeIndex++;
					}
				}
				boxIndex = -1;
				pokeIndex = -1;
			}
			if (boxIndex == -1) {
				pokeIndex++;
				if (pokePC.Party != null && pokeIndex < pokePC.Party.NumPokemon)
					return true;
				boxIndex = 0;
				pokeIndex = -1;
			}
			while (boxIndex < pokePC.NumBoxes) {
				pokeIndex++;
				while (pokeIndex < pokePC[boxIndex].NumSlots) {
					if (pokePC[boxIndex][pokeIndex] != null)
						return true;
					pokeIndex++;
				}
				boxIndex++;
				pokeIndex = -1;
			}
			return false;
		}
		public void Reset() {
			boxIndex	= -1;
			pokeIndex	= -1;
		}

		object IEnumerator.Current {
			get { return Current; }
		}
		void IDisposable.Dispose() { }
	}
}
