﻿using System;
using System.Collections.Generic;
using MaplePacketLib2.Tools;
using MapleServer2.Servers.Game;
using MapleServer2.Types;

namespace MapleServer2.Data
{
    public class PartyManager
    {
        private readonly Dictionary<long, Party> partyList;

        public PartyManager()
        {
            this.partyList = new Dictionary<long, Party>();
        }

        public void AddParty(Party party)
        {
            partyList.Add(party.Id, party);
        }

        public void RemoveParty(Party party)
        {
            partyList.Remove(party.Id);
        }

        public List<Party> GetPartyFinderList(Player player)
        {
            List<Party> results = new();
            foreach (KeyValuePair<long, Party> entry in partyList)
            {
                if (entry.Value.PartyFinderId == 0)
                {
                    continue;
                }

                //Hide Join Party button for self.
                Party newParty = (Party)entry.Value.Clone();
                if (newParty.Members.Contains(player))
                {
                    newParty.Approval = false;
                }

                results.Add(newParty);
            }
            return results;
        }

        public Party GetPartyById(long id)
        {
            Party foundParty;
            if (partyList.TryGetValue(id, out foundParty))
            {
                return foundParty;
            }
            return null;
        }

        public Party GetPartyByLeader(Player leader)
        {
            foreach (KeyValuePair<long, Party> entry in partyList)
            {
                if (entry.Value.Leader.CharacterId == leader.CharacterId)
                {
                    return entry.Value;
                }
            }
            return null;
        }
    }
}