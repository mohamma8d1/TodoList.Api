using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Common.Interfaces;

public interface IQuery<out TResult> : IRequest<TResult> { }
