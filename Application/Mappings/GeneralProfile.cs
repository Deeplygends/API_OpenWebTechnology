using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Application.DTOs;
using Application.Features.Contacts.Commands;
using Application.Features.Contacts.Commands.CreateContact;
using Application.Features.Contacts.Queries.GetAllContacts;
using Application.Features.Skill.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateContactCommand, Contact>();
            CreateMap<UpdateContactCommand, Contact>();
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Skill, SkillDto>();
            CreateMap<SkillDto, Skill>();
            CreateMap<UpdateSkillCommand, Skill>();
            CreateMap<CreateSkillCommand, Skill>();
        }
    }
}
