using Microsoft.AspNetCore.Blazor.Components;
using Blazor.Extensions;
using System.Drawing;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Butterflies.Client.Services;
using Butterflies.Shared;

namespace Butterflies.Client.Pages
{
    public class ButterflyComponent : BlazorComponent
    {
        private const int canvasHeight = 800;
        private const int canvasWidth = 800;
        private const int period = 24;

        protected List<ButterflyDto> _butterflyList = new List<ButterflyDto>();
        protected ButterflyDto _selectedButterfly = new ButterflyDto();
        private Canvas2dContext _context;
        protected BECanvasComponent _canvasReference;

        [Inject]
        protected IButterflyApi ButterflyApi { get; set; }



        protected override async Task OnInitAsync()
        {           
            _butterflyList = await ButterflyApi.GetAllAsync();
            Console.WriteLine($"Fant {_butterflyList.Count} butterflies");

            this._context = this._canvasReference.CreateCanvas2d();
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync()
        {
            await PaintButterfly();
        }

        protected void SelectButterfly(ButterflyDto butterfly)
        {
            _selectedButterfly = butterfly;
        }

        protected async Task DeleteButterfly(ButterflyDto butterfly)
        {
            await ButterflyApi.DeleteAsync(butterfly.Id);
            await RefreshButterflyList();
        }

        private async Task RefreshButterflyList()
        {
            _butterflyList = await ButterflyApi.GetAllAsync();
        }

        protected async Task PaintButterfly()
        {
            await Task.Run(() => DoPaint());
        }

        protected async Task SaveButterfly()
        {
            if(_selectedButterfly == null)
            {
                return;
            }
            var saved = await ButterflyApi.SaveAsync(_selectedButterfly);
            Console.WriteLine("Saved with id " + saved.Id);
            await RefreshButterflyList();
        }

        protected void NewButterfly()
        {
            var newButterfly = new ButterflyDto();
            newButterfly.Id = Guid.NewGuid().ToString();
            _selectedButterfly = newButterfly;
        }
        
        protected void DoPaint()
        {
            this._context.FillStyle = "black";
            this._context.FillRect(0, 0, canvasWidth, canvasHeight);

            if (_selectedButterfly == null)
            {
                return;
            }

            int i;
            double u;
            PointF point, lastPoint;

            int n = _selectedButterfly.N;
            var color = _selectedButterfly.Color;
            var scale = _selectedButterfly.Scale;
            var numWings = _selectedButterfly.NumWings;

            for (i = 0; i < n; i++)
            {
                u = i * period * Math.PI / n;
                point.X = (float) (scale * Math.Sin(u) * (Math.Exp(Math.Cos(u)) - 3 * Math.Cos(numWings * u) - Math.Pow(Math.Sin(u / 10.0), 5.0)));
                point.Y = (float) (scale * Math.Cos(u) * (Math.Exp(Math.Cos(u)) - 3 * Math.Cos(numWings * u) - Math.Pow(Math.Sin(u / 10.0), 5.0)));
    
                point.X = point.X + canvasWidth / 2;
                point.Y = point.Y + canvasHeight / 2;
                if (i > 0)
                {
                    _context.BeginPath();
                    _context.StrokeStyle = color;
                    this._context.MoveTo(lastPoint.X, lastPoint.Y);
                    _context.LineTo(point.X, point.Y);
                    _context.Stroke();
                }
                lastPoint = point;                
            }
        }
    }
}