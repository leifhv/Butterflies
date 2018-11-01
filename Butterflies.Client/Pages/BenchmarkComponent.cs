using Microsoft.AspNetCore.Blazor.Components;
using Blazor.Extensions;
using System.Drawing;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.JSInterop;
using Butterflies.Shared;
using System.Net.Http;
using Microsoft.AspNetCore.Blazor;
using System.Linq;

namespace Butterflies.Client.Pages
{
    public class BenchmarkComponent : BlazorComponent
    {
        private int[] _numbers;
        private const int NumNumbers = 10000;
        [Inject]
        HttpClient Http { get; set; }

        public string NumbersText { get; set; }
        public string StatusText { get; set; }

        public BenchmarkComponent()
        {
            InitBenchmark();
        }

        protected override void OnInit()
        {
            
            base.OnInit();
        }

        private void UpdateNumbersText()
        {
            string tmpNumbersText = "";
            for (int t = 0; t < 100; t++)
            {
                tmpNumbersText += _numbers[t] + ",";
            }
            NumbersText = tmpNumbersText;            
        }

        protected void InitBenchmark()
        {
            _numbers = new int[NumNumbers];
            for (int t = 0; t < NumNumbers; t++)
            {
                _numbers[t] = NumNumbers - t;
            }
            UpdateNumbersText();
            StatusText = "";
        }


        public void RunBlazorBubblesort()
        {
            var sortTask = new SortTask();
            sortTask.Integers = _numbers;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            BubbleSorter.Sort(sortTask);

            stopWatch.Stop();
            UpdateNumbersText();
            StatusText = $"Sorterte { _numbers.Length} tall på {stopWatch.ElapsedMilliseconds} ms med Blazor";
        }

        public async void RunJsBubblesort()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _numbers = await JSRuntime.Current.InvokeAsync<int[]>("sort", _numbers);
            stopWatch.Stop();
            StatusText = $"Sorterte { _numbers.Length} tall på {stopWatch.ElapsedMilliseconds} ms med Javascript";
            UpdateNumbersText();
            this.StateHasChanged();
        }

        public async void RunServerBubblesort()
        {
            SortTask task = new SortTask();
            task.Integers = _numbers;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _numbers = await Http.PostJsonAsync<int[]>("api/Sort/BubbleSort",task);
            stopWatch.Stop();
            UpdateNumbersText();
            StatusText = $"Sorterte { _numbers.Length} tall på {stopWatch.ElapsedMilliseconds} ms på serveren";
            StateHasChanged();
        }


    }
}