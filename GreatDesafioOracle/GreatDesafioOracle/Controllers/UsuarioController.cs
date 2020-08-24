﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatDesafioOracle.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreatDesafioOracle.Controllers
{
    public class UsuarioController : Controller
    {
        private static ModelContext _context;
        public UsuarioController(ModelContext context)
        {
            _context = context;
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<Usuario> GetAll()
        {
            var usuarios = _context.Usuario.OrderBy(a => a.Nome).ToList();
            return usuarios;
        }

        //criar usuário
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public string CadastrarUsuario([Microsoft.AspNetCore.Mvc.FromBody]Usuario usuario)
        {
            try
            {

                if (usuario.Cpf == null || usuario.Nome == null || usuario.Rg == null || usuario.Datanascimento == null)
                {
                    return "Dados devem ser preenchidos";
                }
                var checkUsuario = _context.Usuario.FirstOrDefault(a => a.Cpf.Equals(usuario.Cpf));
                if (checkUsuario == null)
                {
                    usuario.Datacadastro = DateTime.Now.Date;
                    _context.Usuario.Add(usuario);
                    _context.SaveChanges();

                    return "Usuário Salvo";
                }
                return "Usuário já criado";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }

        //deletar usuário
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public string RemoverUsuario(string cpf)
        {

            var checkUsuario = _context.Usuario.FirstOrDefault(a => a.Cpf.Equals(cpf));
            if (checkUsuario != null)
            {
                _context.Usuario.Remove(checkUsuario);
                _context.SaveChanges();

                return "Usuario Deletado";
            }
            return "Usuario não encontrado";
        }

        //pegar um único usuário pelo cpf
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Usuario ConsultarUsuario(string cpf)
        {

            var checkUsuario = _context.Usuario.FirstOrDefault(a => a.Cpf.Equals(cpf));

            return checkUsuario;
        }

        //buscar usuários com mesmo nome
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<Usuario> BuscarUsuario(string nome)
        {

            var checkUsuario = _context.Usuario.Where(a => a.Nome.Contains(nome)).ToList();

            return checkUsuario;
        }        

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult ListarTodosOsUsuarios()
        {
            var usuarios = _context.Usuario.OrderBy(a => a.Nome).ToList();
            ViewData["Usuarios"] = usuarios;
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult DetalhesUsuario(string cpf)
        {
            var checkUsuario = _context.Usuario.FirstOrDefault(a => a.Cpf.Equals(cpf));
            ViewData["Usuario"] = checkUsuario;
            return View(checkUsuario);
        }
    }
}