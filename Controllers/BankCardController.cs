﻿using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.EfStuff.Repositories.IRepository;
using LearnEnglish.Models;
using LearnEnglish.Models.BankCard;
using LearnEnglish.Models.User;
using LearnEnglish.Models.UserProfile;
using LearnEnglish.Services;
using LearnEnglish.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class BankCardController : Controller
    {
        private readonly IBankCardRepository _bankCardRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public BankCardController(IBankCardRepository bankCardRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IFileService fileService, IUserService userService)
        {
            _bankCardRepository = bankCardRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _fileService = fileService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult BankCardGet()
        {
            var user = _userService.GetCurrent();
            var cards = _bankCardRepository.GetByUserId(user.Id);
            var viewModels = _mapper.Map<List<BankCardGetViewModel>>(cards);

            //if (card == null)
            //{
            //    throw new ArgumentNullException(nameof(card), $"Карты с id={id} нет в базе данных");
            //}

            return View(viewModels);
        }

        [Authorize]
        [HttpGet]
        public IActionResult BankCardAll(int page = 1, int perpage = 10)
        {
            var allCards = _bankCardRepository.AllWithPage(page, perpage);
            var viewModels = _mapper.Map<List<BankCardGetViewModel>>(allCards);

            var viewModel = new BankCardAllViewModel()
            {
                Page = page,
                RecordPerPage = perpage,
                TotalRecordCount = _bankCardRepository.Count(),
                BankCards = viewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult BankCardAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardAdd(BankCardAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var newCard = _mapper.Map<BankCard>(viewModel);
            newCard.Owner = _userService.GetCurrent();
            _bankCardRepository.Save(newCard);

            return RedirectToAction("BankCardGet");
            //return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet]
        public IActionResult BankCardRemove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardRemove(long id)
        {
            var card = _bankCardRepository.Get(id);

            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), $"Карты с id={id} нет в базе данных");
            }

            _bankCardRepository.Remove(card);

            return RedirectToAction("BankCardAll");
        }

        [HttpGet]
        public IActionResult BankCardDelete(long id)
        {
            var card = _bankCardRepository.Get(id);
            _bankCardRepository.Remove(card);

            return RedirectToAction("BankCardAll");
        }

        [HttpGet]
        public IActionResult DownloadBankCard()
        {
            var pathToFile = _fileService.GetTempDocxFilePath();
            var allCards = _bankCardRepository.GetAll();

            using (var file = DocX.Create(pathToFile))
            {
                Table table = file.AddTable(allCards.Count + 1, 5);
                table.Alignment = Alignment.center;
                table.Design = TableDesign.TableGrid;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Id card");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Card Number");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Validity Month");
                table.Rows[0].Cells[3].Paragraphs.First().Append("Validity Year");
                table.Rows[0].Cells[4].Paragraphs.First().Append("Owner ID");

                for (int i = 1; i <= allCards.Count; i++)
                {
                    table.Rows[i].Cells[0].Paragraphs.First().Append(allCards[i - 1].Id.ToString());
                    table.Rows[i].Cells[1].Paragraphs.First().Append(allCards[i - 1].CardNumber);
                    table.Rows[i].Cells[2].Paragraphs.First().Append(allCards[i - 1].ValidityMonth.ToString());
                    table.Rows[i].Cells[3].Paragraphs.First().Append(allCards[i - 1].ValidityYear.ToString());
                    table.Rows[i].Cells[4].Paragraphs.First().Append(allCards[i - 1].OwnerId.ToString());
                }

                file.InsertTable(table);
                file.Save();
            }

            return PhysicalFile(pathToFile,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "BankCardAll.docx");
        }

        [HttpGet]
        public IActionResult CreateBankCardsForTrain()
        {
            var user = _userService.GetCurrent();

            for (var i = 0; i < 50; i++)
            {
                var bankCard = new BankCard()
                {
                    CardNumber = "4855777788889997",
                    ValidityMonth = 5,
                    ValidityYear = 2025,
                    Owner = user
                };

                _bankCardRepository.Save(bankCard);
            }

            return RedirectToAction("BankCardAll");
        }
    }
}
