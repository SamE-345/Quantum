using Microsoft.AspNetCore.Routing.Matching;
using Quantum;
using Quantum.Core;
namespace Quantum_Circuit_WebApp.Services
{
    
    public class CircuitService
    {
        private QuantumCircuit? _circuit;
        public bool hasCircuit => _circuit != null;
        public int qubitCount => _circuit?.qubitCount ?? 0;
        public void CreateCircuit(int qubits)
        {
            _circuit = new QuantumCircuit(qubits);
        }
        public void AddGate(string gateName, params int[] targets)
        {
            if(_circuit == null)
            {
                throw new Exception("Circuit not created");
            }
            IGate gate = gateName switch
            {
                "X" => new XGate(),
                "Y" => new YGate(),
                "Z" => new ZGate(),
                //Add More gates 
                _ => throw new Exception("Unknown Gate")
            };
            _circuit.AddGate(gate, targets);
        }
        public void Run()
        {
            _circuit?.Run();
        }
        public ComplexNum[] GetState()
        {
            return _circuit?.GetState() ?? Array.Empty<ComplexNum>();
        }
    }
}
