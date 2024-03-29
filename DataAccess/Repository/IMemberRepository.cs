﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberByID(int MemberID);
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int MemberID);
        Member GetMemberByEmail(string email);
        Member CheckAccount(string email, string password);
    }
}
