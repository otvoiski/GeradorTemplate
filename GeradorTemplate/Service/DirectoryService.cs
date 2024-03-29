﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeradorTemplate.Service
{
    public interface IDirectoryService
    {
        string[] GetDirectories(string nomeProjeto);
        void CreateDirectory(string newPath);
        IEnumerable<string> EnumerateFiles();
        void MoveDirectory(string tipoProjeto, string nomeProjeto);
    }

    public class DirectoryService : IDirectoryService
    {
        IConstante Constante;

        public DirectoryService(
            IConstante Constante
        )
        {
            this.Constante = Constante;
        }

        public string[] GetDirectories(string nomeProjeto)
        {
            var directories = Directory.GetDirectories(Constante.GetPathTemplate(), "*.*", SearchOption.AllDirectories);
            directories = directories.Select(x => x.Replace(Constante.GetNameTemplate(), nomeProjeto)).ToArray();
            return directories;
        }

        public void CreateDirectory(string newPath)
        {
            Directory.CreateDirectory(newPath);
        }

        public IEnumerable<string> EnumerateFiles()
        {
            return Directory.EnumerateFiles($"{Constante.GetPathTemplate()}{Constante.GetIdentifier()}", "*.*", SearchOption.AllDirectories);
        }

        public void MoveDirectory(string tipoProjeto, string nomeProjeto)
        {
            CreateDirectory(Constante.GetFullPath(tipoProjeto, nomeProjeto));
            Directory.Move(
                $"{Constante.GetPathTemplate()}{Constante.GetPathOutput()}",
                $"{Constante.GetFullPath(tipoProjeto, nomeProjeto)}\\{Constante.GetIdentifier()}"
            );
        }
    }
}
