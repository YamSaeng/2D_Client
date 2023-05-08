using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace ServerCore
{
    public class CMessage : IDisposable
    {
        private enum en_MessageBufSize
        {
            MESSAGE_BUF_SIZE = 100000
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct st_ENCODE_HEADER
        {
            public byte PacketCode;

            public short PacketLen;

            public byte RandXORCode;
            public byte CheckSum;
        };

        public byte[] _MessageBuf = new byte[(int)en_MessageBufSize.MESSAGE_BUF_SIZE];

        MemoryStream _MessageWriteStream;
        MemoryStream _MessageReadStream;

        BinaryWriter _MessageBufWriter;
        BinaryReader _MessageBufReader;

        int _Front;
        int _Rear;
        int _Header;

        int _BufferSize;
        int _UseBufferSize;
        byte _Key;

        public CMessage()
        {
            _Front = 5;
            _Rear = 5;
            _Header = 0;
            _BufferSize = (int)en_MessageBufSize.MESSAGE_BUF_SIZE;
            _UseBufferSize = 0;
            _Key = 50;

            _MessageWriteStream = new MemoryStream(_MessageBuf);
            _MessageReadStream = new MemoryStream(_MessageBuf);

            _MessageBufWriter = new BinaryWriter(_MessageWriteStream);
            _MessageBufReader = new BinaryReader(_MessageReadStream);

            _MessageReadStream.Position = _Front;
            _MessageWriteStream.Position = _Rear;
        }

        public void Clear()
        {
            _Front = 5;
            _Rear = 5;

            _MessageReadStream.Position = _Front;
            _MessageWriteStream.Position = _Rear;

            _Header = 0;
            _UseBufferSize = 0;
        }

        public int GetRear()
        {
            return _Rear;
        }

        public void Dispose()
        {
            _MessageWriteStream = null;
            _MessageReadStream = null;
            _MessageBufWriter = null;
            _MessageBufReader = null;
            _MessageBuf = null;
        }

        public int GetBufferSize()
        {
            return _BufferSize;
        }

        public int GetUseBufferSize()
        {
            return _UseBufferSize;
        }

        public void MoveWritePosition(int Size)
        {
            _Rear += Size;
            _UseBufferSize += Size;
        }

        public void MoveReadPosition(int Size)
        {
            _Front += Size;
            _UseBufferSize -= Size;
        }

        public void InsertData(byte[] Data, int Position, int Size)
        {
            Buffer.BlockCopy(Data, 0, _MessageBuf, Position, Size);
            _Rear += Size;
            _UseBufferSize += Size;
        }

        public int InsertData(byte Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int InsertData(byte[] Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int InsertData(bool Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int InsertData(string Data)
        {
            _MessageWriteStream.Position = _Rear;
            byte[] DataToBytes = System.Text.Encoding.Unicode.GetBytes(Data);
            short DataLen = (short)DataToBytes.Length;
            _MessageBufWriter.Write(DataLen);
            _MessageBufWriter.Write(DataToBytes);

            _Rear += (DataToBytes.Length + sizeof(short));
            _UseBufferSize += (DataToBytes.Length + sizeof(short));
            return Data.Length;
        }

        public int InsertData(short Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int InsertData(int Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int InsertData(long Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int InsertData(float Data, int Size)
        {
            _MessageWriteStream.Position = _Rear;
            _MessageBufWriter.Write(Data);

            _Rear += Size;
            _UseBufferSize += Size;
            return Size;
        }

        public int GetData(out byte Data, int Size)
        {
            Data = _MessageBufReader.ReadByte();

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= Size;
            return Size;
        }

        public void GetData(byte[] Data, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                Data[i] = _MessageBufReader.ReadByte();
                _UseBufferSize -= sizeof(byte);
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public int GetData(out bool Data, int Size)
        {
            Data = _MessageBufReader.ReadBoolean();

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= Size;
            return Size;
        }

        public int GetData(out short Data, int Size)
        {
            Data = _MessageBufReader.ReadInt16();

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= Size;
            return Size;
        }

        public void GetData(short[] Data, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                Data[i] = _MessageBufReader.ReadInt16();
                _UseBufferSize -= sizeof(short);
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public int GetData(out int Data, int Size)
        {
            Data = _MessageBufReader.ReadInt32();

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= Size;
            return Size;
        }

        public int GetData(out float Data, int Size)
        {
            Data = _MessageBufReader.ReadSingle();

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= Size;
            return Size;
        }

        public int GetData(out long Data, int Size)
        {
            Data = _MessageBufReader.ReadInt64();

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= Size;
            return Size;
        }

        public void GetData(long[] Data, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                Data[i] = _MessageBufReader.ReadInt64();
                _UseBufferSize -= sizeof(long);
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public int GetData(out string Data)
        {
            // string 길이 읽기
            short Len = _MessageBufReader.ReadInt16();
            // 길이 만큼 byte로 읽기
            byte[] Bytes = _MessageBufReader.ReadBytes(Len);
            // 읽은 byte를 string으로 변환해서 Data에 저장
            Data = Encoding.Unicode.GetString(Bytes);

            _Front = (int)_MessageReadStream.Position;
            _UseBufferSize -= (Bytes.Length + sizeof(short));
            return Bytes.Length + sizeof(byte);
        }

        public void GetData(out st_GameObjectInfo Data)
        {
            st_GameObjectInfo GameObjectInfo = new st_GameObjectInfo();

            GameObjectInfo.ObjectId = _MessageBufReader.ReadInt64();

            short Len = _MessageBufReader.ReadInt16();
            byte[] Bytes = _MessageBufReader.ReadBytes(Len);
            GameObjectInfo.ObjectName = Encoding.Unicode.GetString(Bytes);

            GameObjectInfo.ObjectCropStep = _MessageBufReader.ReadByte();
            GameObjectInfo.ObjectCropMaxStep = _MessageBufReader.ReadByte();
            GameObjectInfo.ObjectSkillPoint = _MessageBufReader.ReadByte();
            GameObjectInfo.ObjectPositionInfo.State = (en_CreatureState)_MessageBufReader.ReadByte();
            GameObjectInfo.ObjectPositionInfo.CollisitionPosition.x = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectPositionInfo.CollisitionPosition.y = _MessageBufReader.ReadInt32();            
            GameObjectInfo.ObjectPositionInfo.Position.x = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectPositionInfo.Position.y = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectPositionInfo.LookAtDireciton.x = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectPositionInfo.LookAtDireciton.y = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectPositionInfo.MoveDireciton.x = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectPositionInfo.MoveDireciton.y = _MessageBufReader.ReadSingle();

            GameObjectInfo.ObjectStatInfo.Level = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.HP = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.MaxHP = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.MP = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.MaxMP = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.DP = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.MaxDP = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.AutoRecoveyHPPercent = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.AutoRecoveyMPPercent = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.MinMeleeAttackDamage = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.MaxMeleeAttackDamage = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.MeleeAttackHitRate = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.MagicDamage = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.MagicHitRate = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectStatInfo.Defence = _MessageBufReader.ReadInt32();
            GameObjectInfo.ObjectStatInfo.EvasionRate = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.MeleeCriticalPoint = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.MagicCriticalPoint = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.StatusAbnormalResistance = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectStatInfo.Speed = _MessageBufReader.ReadSingle();
            GameObjectInfo.ObjectStatInfo.MaxSpeed = _MessageBufReader.ReadSingle();

            GameObjectInfo.ObjectType = (en_GameObjectType)_MessageBufReader.ReadInt16();
            GameObjectInfo.OwnerObjectId = _MessageBufReader.ReadInt64();
            GameObjectInfo.OwnerObjectType = (en_GameObjectType)_MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectWidth = _MessageBufReader.ReadInt16();
            GameObjectInfo.ObjectHeight = _MessageBufReader.ReadInt16();
            GameObjectInfo.PlayerSlotIndex = _MessageBufReader.ReadByte();

            Data = GameObjectInfo;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(st_GameObjectInfo[] Data, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                st_GameObjectInfo GameObjectInfo;

                GetData(out GameObjectInfo);

                Data[i] = GameObjectInfo;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_QuickSlotBarSlotInfo Data)
        {
            st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo = new st_QuickSlotBarSlotInfo();

            QuickSlotBarSlotInfo.QuickSlotBarType = (en_QuickSlotBarType)_MessageBufReader.ReadByte();
            QuickSlotBarSlotInfo.AccountDBId = _MessageBufReader.ReadInt64();
            QuickSlotBarSlotInfo.PlayerDBId = _MessageBufReader.ReadInt64();
            QuickSlotBarSlotInfo.QuickSlotBarIndex = _MessageBufReader.ReadByte();
            QuickSlotBarSlotInfo.QuickSlotBarSlotIndex = _MessageBufReader.ReadByte();
            QuickSlotBarSlotInfo.QuickSlotKey = (KeyCode)_MessageBufReader.ReadInt16();

            bool EmptyQuickSlotSkillInfo;
            EmptyQuickSlotSkillInfo = _MessageBufReader.ReadBoolean();

            if (EmptyQuickSlotSkillInfo == false)
            {
                st_SkillInfo QuickSlotSkillInfo;

                GetData(out QuickSlotSkillInfo);

                QuickSlotBarSlotInfo.QuickBarSkillInfo = QuickSlotSkillInfo;
            }
            else
            {
                QuickSlotBarSlotInfo.QuickBarSkillInfo = null;
            }

            bool EmptyQuickSlotItemInfo;
            EmptyQuickSlotItemInfo = _MessageBufReader.ReadBoolean();

            if (EmptyQuickSlotItemInfo == false)
            {
                st_ItemInfo QuickSlotItemInfo;

                GetData(out QuickSlotItemInfo);

                QuickSlotBarSlotInfo.QuickBarItemInfo = QuickSlotItemInfo;
            }
            else
            {
                QuickSlotBarSlotInfo.QuickBarItemInfo = null;
            }

            _Front = (int)_MessageReadStream.Position;

            Data = QuickSlotBarSlotInfo;
        }

        public void GetData(st_QuickSlotBarSlotInfo[] Data, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo;

                GetData(out QuickSlotBarSlotInfo);

                Data[i] = QuickSlotBarSlotInfo;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_PositionInfo Data)
        {
            st_PositionInfo DataParsing = new st_PositionInfo();

            DataParsing.State = (en_CreatureState)_MessageBufReader.ReadByte();
            DataParsing.CollisitionPosition.x = _MessageBufReader.ReadInt32();
            DataParsing.CollisitionPosition.y = _MessageBufReader.ReadInt32();            
            DataParsing.Position.x = _MessageBufReader.ReadSingle();
            DataParsing.Position.y = _MessageBufReader.ReadSingle();                        
            DataParsing.LookAtDireciton.x = _MessageBufReader.ReadSingle();
            DataParsing.LookAtDireciton.y = _MessageBufReader.ReadSingle();
            DataParsing.MoveDireciton.x = _MessageBufReader.ReadSingle();
            DataParsing.MoveDireciton.y = _MessageBufReader.ReadSingle();

            _Front = (int)_MessageReadStream.Position;

            Data = DataParsing;
            DataParsing = null;
        }

        public void GetData(out st_StatInfo Data)
        {
            st_StatInfo DataParsing = new st_StatInfo();

            DataParsing.Level = _MessageBufReader.ReadInt32();
            DataParsing.HP = _MessageBufReader.ReadInt32();
            DataParsing.MaxHP = _MessageBufReader.ReadInt32();
            DataParsing.MP = _MessageBufReader.ReadInt32();
            DataParsing.MaxMP = _MessageBufReader.ReadInt32();
            DataParsing.DP = _MessageBufReader.ReadInt32();
            DataParsing.MaxDP = _MessageBufReader.ReadInt32();
            DataParsing.AutoRecoveyHPPercent = _MessageBufReader.ReadInt16();
            DataParsing.AutoRecoveyMPPercent = _MessageBufReader.ReadInt16();
            DataParsing.MinMeleeAttackDamage = _MessageBufReader.ReadInt32();
            DataParsing.MaxMeleeAttackDamage = _MessageBufReader.ReadInt32();
            DataParsing.MeleeAttackHitRate = _MessageBufReader.ReadInt16();
            DataParsing.MagicDamage = _MessageBufReader.ReadInt16();
            DataParsing.MagicHitRate = _MessageBufReader.ReadSingle();
            DataParsing.Defence = _MessageBufReader.ReadInt32();
            DataParsing.EvasionRate = _MessageBufReader.ReadInt16();
            DataParsing.MeleeCriticalPoint = _MessageBufReader.ReadInt16();
            DataParsing.MagicCriticalPoint = _MessageBufReader.ReadInt16();
            DataParsing.StatusAbnormalResistance = _MessageBufReader.ReadInt16();
            DataParsing.Speed = _MessageBufReader.ReadSingle();
            DataParsing.MaxSpeed = _MessageBufReader.ReadSingle();

            _Front = (int)_MessageReadStream.Position;

            Data = DataParsing;
        }

        public void GetData(out CItem Item)
        {
            CItem NewItem = new CItem();

            st_ItemInfo ItemInfo;

            GetData(out ItemInfo);

            NewItem._ItemInfo = ItemInfo;
            Item = NewItem;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(CItem[] Items, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                CItem NewItem = new CItem();

                st_ItemInfo ItemInfo;

                GetData(out ItemInfo);

                NewItem._ItemInfo = ItemInfo;
                Items[i] = NewItem;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_SkillInfo Data)
        {
            st_SkillInfo SkillInfo = new st_SkillInfo();

            SkillInfo.CanSkillUse = _MessageBufReader.ReadBoolean();
            SkillInfo.IsSkillLearn = _MessageBufReader.ReadBoolean();
            SkillInfo.SkillCharacteristic = (en_SkillCharacteristic)_MessageBufReader.ReadByte();
            SkillInfo.SkillLargeCategory = (en_SkillLargeCategory)_MessageBufReader.ReadByte();
            SkillInfo.SkillMediumCategory = (en_SkillMediumCategory)_MessageBufReader.ReadByte();
            SkillInfo.SkillType = (en_SkillType)_MessageBufReader.ReadInt16();
            SkillInfo.SkillLevel = _MessageBufReader.ReadByte();
            SkillInfo.SkillMinDamage = _MessageBufReader.ReadInt32();
            SkillInfo.SkillMaxDamage = _MessageBufReader.ReadInt32();
            SkillInfo.SkillOverlapStep = _MessageBufReader.ReadByte();

            short SkillNameLen = _MessageBufReader.ReadInt16();
            byte[] SkillNameBytes = _MessageBufReader.ReadBytes(SkillNameLen);
            SkillInfo.SkillName = Encoding.Unicode.GetString(SkillNameBytes);

            SkillInfo.SkillCoolTime = _MessageBufReader.ReadInt32();
            SkillInfo.SkillCastingTime = _MessageBufReader.ReadInt32();
            SkillInfo.SkillDurationTime = _MessageBufReader.ReadInt64();
            SkillInfo.SkillDotTime = _MessageBufReader.ReadInt64();
            SkillInfo.SkillRemainTime = _MessageBufReader.ReadInt64();            

            Data = SkillInfo;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(st_SkillInfo[] Skills, int SkillCount)
        {
            for (int i = 0; i < SkillCount; i++)
            {
                st_SkillInfo SkillInfo;

                GetData(out SkillInfo);

                Skills[i] = SkillInfo;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_ItemInfo Data)
        {
            st_ItemInfo ItemInfo = new st_ItemInfo();

            ItemInfo.ItemDBId = _MessageBufReader.ReadInt64();
            ItemInfo.IsEquipped = _MessageBufReader.ReadBoolean();
            ItemInfo.ItemWidth = _MessageBufReader.ReadInt16();
            ItemInfo.ItemHeight = _MessageBufReader.ReadInt16();
            ItemInfo.ItemTileGridPositionX = _MessageBufReader.ReadInt16();
            ItemInfo.ItemTileGridPositionY = _MessageBufReader.ReadInt16();

            ItemInfo.OwnerCraftingTable = (en_UIObjectInfo)(_MessageBufReader.ReadInt16());
            ItemInfo.ItemLargeCategory = (en_LargeItemCategory)(_MessageBufReader.ReadByte());
            ItemInfo.ItemMediumCategory = (en_MediumItemCategory)(_MessageBufReader.ReadByte());
            ItemInfo.ItemSmallCategory = (en_SmallItemCategory)(_MessageBufReader.ReadInt16());

            ItemInfo.ItemEquipmentPart = (en_EquipmentParts)(_MessageBufReader.ReadByte());

            short ItemNameLen = _MessageBufReader.ReadInt16();
            byte[] ItemNameBytes = _MessageBufReader.ReadBytes(ItemNameLen);
            ItemInfo.ItemName = Encoding.Unicode.GetString(ItemNameBytes);

            short ItemExplainLen = _MessageBufReader.ReadInt16();
            byte[] ItemExplainBytes = _MessageBufReader.ReadBytes(ItemExplainLen);
            ItemInfo.ItemExplain = Encoding.Unicode.GetString(ItemExplainBytes);

            ItemInfo.ItemCraftingTime = _MessageBufReader.ReadInt64();
            ItemInfo.ItemCraftingRemainTime = _MessageBufReader.ReadInt64();
            ItemInfo.ItemMinDamage = _MessageBufReader.ReadInt32();
            ItemInfo.ItemMaxDamage = _MessageBufReader.ReadInt32();
            ItemInfo.ItemDefence = _MessageBufReader.ReadInt32();
            ItemInfo.ItemMaxCount = _MessageBufReader.ReadInt32();

            ItemInfo.ItemCount = _MessageBufReader.ReadInt16();

            ItemInfo.IsEquipped = _MessageBufReader.ReadBoolean();

            short ItemCraftingMaterialCount = _MessageBufReader.ReadInt16();
            if (ItemCraftingMaterialCount > 0)
            {
                for (int i = 0; i < ItemCraftingMaterialCount; i++)
                {
                    st_CraftingMaterialItemInfo CraftingMaterialItem = new st_CraftingMaterialItemInfo();
                    CraftingMaterialItem.MaterialItemType = (en_SmallItemCategory)_MessageBufReader.ReadInt16();

                    short CraftingMaterialNameLen = _MessageBufReader.ReadInt16();
                    byte[] CraftingMaterialNameBytes = _MessageBufReader.ReadBytes(CraftingMaterialNameLen);
                    CraftingMaterialItem.MaterialItemName = Encoding.Unicode.GetString(CraftingMaterialNameBytes);

                    CraftingMaterialItem.ItemCount = _MessageBufReader.ReadInt16();

                    ItemInfo.Materials.Add(CraftingMaterialItem);
                }
            }

            Data = ItemInfo;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_Color Data)
        {
            st_Color Color = new st_Color();

            Color._Red = _MessageBufReader.ReadInt16();
            Color._Green = _MessageBufReader.ReadInt16();
            Color._Blue = _MessageBufReader.ReadInt16();

            _Front = (int)_MessageReadStream.Position;
            Data = Color;
        }

        public void GetData(st_CraftingItemCategory[] Data, int Count)
        {
            for (int i = 0; i < Count; ++i)
            {
                st_CraftingItemCategory CraftingItemCategory = new st_CraftingItemCategory();

                CraftingItemCategory.CategoryType = (en_LargeItemCategory)_MessageBufReader.ReadByte();

                short CategoryNameLen = _MessageBufReader.ReadInt16();
                byte[] CategoryNameBytes = _MessageBufReader.ReadBytes(CategoryNameLen);
                CraftingItemCategory.CategoryName = Encoding.Unicode.GetString(CategoryNameBytes);

                byte CraftingCompleteItemCount = _MessageBufReader.ReadByte();

                for (int j = 0; j < CraftingCompleteItemCount; ++j)
                {
                    st_ItemInfo CommonCraftingCompleteItem;

                    GetData(out CommonCraftingCompleteItem);

                    CraftingItemCategory.CommonCraftingCompleteItems.Add(CommonCraftingCompleteItem);
                }

                Data[i] = CraftingItemCategory;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(st_CraftingTableRecipe[] Data, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                st_CraftingTableRecipe CraftingTable = new st_CraftingTableRecipe();

                CraftingTable.CraftingTableType = (en_GameObjectType)_MessageBufReader.ReadInt16();

                short CraftingTableNameLen = _MessageBufReader.ReadInt16();
                byte[] CraftingTableNameBytes = _MessageBufReader.ReadBytes(CraftingTableNameLen);
                CraftingTable.CraftingTableName = Encoding.Unicode.GetString(CraftingTableNameBytes);

                byte CraftingCompleteItemCount = _MessageBufReader.ReadByte();

                CraftingTable.CraftingTableCompleteItems = new List<st_ItemInfo>();

                for (int j = 0; j < CraftingCompleteItemCount; ++j)
                {
                    st_ItemInfo ItemInfo;

                    GetData(out ItemInfo);

                    CraftingTable.CraftingTableCompleteItems.Add(ItemInfo);
                }

                Data[i] = CraftingTable;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(st_ServerInfo[] ServerLists, byte Count)
        {
            for (int i = 0; i < Count; i++)
            {
                st_ServerInfo ServerList = new st_ServerInfo();

                short ServerNameLen = _MessageBufReader.ReadInt16();
                byte[] ServerNameBytes = _MessageBufReader.ReadBytes(ServerNameLen);
                ServerList.ServerName = Encoding.Unicode.GetString(ServerNameBytes);

                short ServerIPLen = _MessageBufReader.ReadInt16();
                byte[] ServerIPNameBytes = _MessageBufReader.ReadBytes(ServerIPLen);
                ServerList.ServerIP = Encoding.Unicode.GetString(ServerIPNameBytes);

                ServerList.ServerPort = _MessageBufReader.ReadInt32();

                ServerList.ServerBusy = _MessageBufReader.ReadSingle();

                ServerLists[i] = ServerList;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_PersonalMessage Data)
        {
            st_PersonalMessage PersonalMessage = new st_PersonalMessage();
            byte PersonalMessageType = _MessageBufReader.ReadByte();

            short PersonalMessageLen = _MessageBufReader.ReadInt16();
            byte[] PersonalMessageBytes = _MessageBufReader.ReadBytes(PersonalMessageLen);

            PersonalMessage.PersonalMessageType = (en_GlobalMessageType)PersonalMessageType;
            PersonalMessage.PersonalMessage = Encoding.Unicode.GetString(PersonalMessageBytes);

            Data = PersonalMessage;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(st_PersonalMessage[] Data, byte Count)
        {
            for (int i = 0; i < Count; i++)
            {
                st_PersonalMessage PersonalMessage = new st_PersonalMessage();
                byte PersonalMessageType = _MessageBufReader.ReadByte();

                short PersonalMessageLen = _MessageBufReader.ReadInt16();
                byte[] PersonalMessageBytes = _MessageBufReader.ReadBytes(PersonalMessageLen);

                PersonalMessage.PersonalMessageType = (en_GlobalMessageType)PersonalMessageType;
                PersonalMessage.PersonalMessage = Encoding.Unicode.GetString(PersonalMessageBytes);

                Data[i] = PersonalMessage;
            }

            _Front = (int)_MessageReadStream.Position;
        }       

        public void GetData(out st_Day Data)
        {
            st_Day DayInfo = new st_Day();
            DayInfo.DayTimeCycle = _MessageBufReader.ReadSingle();
            DayInfo.DayTimeCheck = _MessageBufReader.ReadSingle();
            DayInfo.DayRatio = _MessageBufReader.ReadSingle();

            DayInfo.DayType = (en_DayType)_MessageBufReader.ReadByte();

            Data = DayInfo;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(out st_RayCastingPosition Data)
        {
            st_RayCastingPosition RayCastingPosition = new st_RayCastingPosition();
            RayCastingPosition.StartPosition.x = _MessageBufReader.ReadSingle();
            RayCastingPosition.StartPosition.y = _MessageBufReader.ReadSingle();
            RayCastingPosition.EndPosition.x = _MessageBufReader.ReadSingle();
            RayCastingPosition.EndPosition.y = _MessageBufReader.ReadSingle();

            Data = RayCastingPosition;

            _Front = (int)_MessageReadStream.Position;
        }

        public void GetData(st_RayCastingPosition[] Datas, byte Count)
        {
            for (int i = 0; i < Count; i++)
            {
                st_RayCastingPosition RayCastingInfo;

                GetData(out RayCastingInfo);

                Datas[i] = RayCastingInfo;
            }

            _Front = (int)_MessageReadStream.Position;
        }

        public void SetHeader(st_ENCODE_HEADER Header)
        {
            int HeaderSize = Marshal.SizeOf(typeof(st_ENCODE_HEADER));
            byte[] StructByteArrays = new byte[HeaderSize];

            GCHandle GCH = GCHandle.Alloc(StructByteArrays, GCHandleType.Pinned);
            IntPtr Ptr = GCH.AddrOfPinnedObject();

            Marshal.StructureToPtr(Header, Ptr, true);

            Array.Copy(StructByteArrays, 0, _MessageBuf, _Header, HeaderSize);

            GCH.Free();

            _UseBufferSize += Marshal.SizeOf(typeof(st_ENCODE_HEADER));
        }

        public bool Encode()
        {
            byte CheckSum = 0;
            int DataLen = _UseBufferSize;
            long Sum = 0;

            System.Random rand = new System.Random();

            //-----------------------------------------------------
            // 헤더 준비
            //-----------------------------------------------------
            st_ENCODE_HEADER EncodeHeader = new st_ENCODE_HEADER();
            EncodeHeader.PacketCode = 119;
            EncodeHeader.PacketLen = (short)_UseBufferSize;
            EncodeHeader.RandXORCode = (byte)rand.Next(0, 256);

            //-----------------------------------------------------
            // 체크섬 계산
            // 데이터의 모든 자리를 다 더함
            //-----------------------------------------------------
            byte[] PayLoadArray = new byte[_UseBufferSize];
            Array.Copy(_MessageBuf, _Front, PayLoadArray, 0, _UseBufferSize);

            for (int i = 0; i < PayLoadArray.Length; i++)
            {
                Sum += PayLoadArray[i];
            }

            //---------------------------
            // 더한 값을 256으로 % 연산
            //---------------------------
            CheckSum = (byte)(Sum % 256);

            //-------------------------------
            // 체크섬 기록
            //-------------------------------
            EncodeHeader.CheckSum = CheckSum;

            //------------------------------------------------------------------
            // 완성된 헤더 셋팅
            //------------------------------------------------------------------            
            SetHeader(EncodeHeader);

            int P1 = 0;
            int E1 = 0;

            for (int i = 0; i < EncodeHeader.PacketLen + 1; i++)
            {
                P1 = (_MessageBuf[_Front - 1 + i]) ^ (P1 + EncodeHeader.RandXORCode + i + 1);
                E1 = (P1) ^ (E1 + _Key + i + 1);
                _MessageBuf[_Front - 1 + i] = (byte)E1;
            }

            rand = null;

            return true;
        }

        public bool Decode(st_ENCODE_HEADER DecodeHeader)
        {
            long Sum = 0;
            byte P1 = 0;
            byte E1 = 0;

            //---------------------------------
            // 패킷 코드 검사
            //---------------------------------
            if (DecodeHeader.PacketCode != 119)
            {
                return false;
            }

            //-----------------------------------------------
            // 길이 검사
            //-----------------------------------------------
            if (DecodeHeader.PacketLen != _UseBufferSize - 5)
            {
                return false;
            }

            //--------------------------------------------------------
            // 체크섬 부터 복호화
            //--------------------------------------------------------            
            byte DecodePoint = 0;

            for (int i = 0; i < DecodeHeader.PacketLen + 1; i++)
            {
                P1 = (byte)((_MessageBuf[_Front - 1 + i]) ^ (DecodePoint + _Key + i + 1));
                DecodePoint = (_MessageBuf[_Front - 1 + i]);
                E1 = (byte)(P1 ^ (E1 + DecodeHeader.RandXORCode + i + 1));
                (_MessageBuf[_Front - 1 + i]) = E1;

                if (i == 0)
                {
                    DecodeHeader.CheckSum = E1;
                }

                E1 = P1;
            }

            //----------------------------------------------------
            // 체크섬 계산
            //----------------------------------------------------
            byte CheckSum = 0;
            for (int i = 0; i < DecodeHeader.PacketLen; i++)
            {
                Sum += (_MessageBuf[_Front + i]);
            }

            CheckSum = (byte)(Sum % 256);

            //-------------------------------------
            // 헤더에 있는 체크섬과 비교 
            //-------------------------------------
            if (DecodeHeader.CheckSum != CheckSum)
            {
                return false;
            }

            return true;
        }
    }
}
