using System;
using System.Collections.Generic;
using System.Text;

namespace engine
{

    namespace core
    {
        sealed public class CardCollection
        {
            public string Description { get => ""; }
            public bool IsEmpty { get => Mask == 0; }
            public uint Size { get => IsEmpty ? 0 : BitMan.BitPopCount(Mask); }
            public ulong Mask { get; private set; } = 0;

            public CardCollection(ulong mask)
            {
                Mask = mask;
            }

            public CardCollection(List<ulong> container)
            {
                for (int i = 0; i < container.Count; i++)
                {
                    Insert(container[i]);
                }
            }

            public CardCollection(params ulong[] args)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    Insert(args[i]);
                }
            }

            public CardCollection Intersect(CardCollection other)
            {
                return new CardCollection(Mask & other.Mask);
            }

            public bool Contain(CardCollection other)
            {
                return (Mask | other.Mask) == Mask;
            }

            public CardCollection Insert(params ulong[] args)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    Mask |= args[i];
                }
                return this;
            }

            public CardCollection Insert(params Card[] cards)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    Mask |= cards[i].Mask;
                }
                return this;
            }

            public CardCollection Insert(CardCollection cards)
            {
                Mask |= cards.Mask;
                return this;
            }

            public CardCollection Erase(CardCollection other)
            {
                Mask &= ~other.Mask;
                return this;
            }

            public CardCollection Combine(CardCollection other)
            {
                Mask |= other.Mask;
                return this;
            }

            public CardCollection Split(CardCollection other)
            {
                return Erase(other);
            }

            public CardCollection Differentitate(CardCollection other)
            {
                Mask ^= other.Mask;
                return this;
            }

            public List<Card> GetAllCards()
            {
                List<Card> result = new List<Card>();
                var currentMask = Mask;
                while (currentMask != 0)
                {
                    var card = new Card(BitMan.BitScanForward(currentMask));
                    result.Add(card);
                    currentMask = BitMan.ClearLowestBit(currentMask);
                }
                return result;
            }

            public Card At(uint index)
            {
                index = System.Math.Min(System.Math.Max(index, 0), Size);

                var temp = Mask;

                // FIXME: index <= 13 only

                switch (index)
                {
                    case 12:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 11;
                    case 11:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 10;
                    case 10:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 9;
                    case 9:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 8;
                    case 8:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 7;
                    case 7:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 6;
                    case 6:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 5;
                    case 5:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 4;
                    case 4:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 3;
                    case 3:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 2;
                    case 2:
                        temp = BitMan.ClearLowestBit(temp);
                        goto case 1;
                    case 1:
                        temp = BitMan.ClearLowestBit(temp);
                        break;
                    case 0:
                        break;
                    default:
                        // FIXMEL ASSERT(false);
                        break;
                }

                return new Card(BitMan.BitScanForward(temp));
            }

            public void SetMask(ulong mask)
            {
                Mask = mask;
            }

            public void Clear()
            {
                Mask = 0;
            }

            #region Operators
            public static bool operator ==(CardCollection lhs, CardCollection rhs)
            {
                return lhs.Mask == rhs.Mask;
            }

            public static bool operator !=(CardCollection lhs, CardCollection rhs)
            {
                return lhs.Mask != rhs.Mask;
            }
            #endregion
        }
    }
}
