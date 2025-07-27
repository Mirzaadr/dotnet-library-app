const Register = () => {
  return (
    <form method="POST" className="flex flex-col">
      <input
        type="text"
        name="name"
        placeholder="Full Name"
        required
        className="p-3 mb-4 border border-gray-300 rounded"
      />
      <input
        type="email"
        name="email"
        placeholder="Email address"
        required
        className="p-3 mb-4 border border-gray-300 rounded"
      />
      <input
        type="password"
        name="password"
        placeholder="Password"
        required
        className="p-3 mb-4 border border-gray-300 rounded"
      />
      <button
        type="submit"
        className="bg-indigo-600 hover:bg-indigo-700 text-white py-3 rounded transition"
      >
        Register
      </button>
    </form>
  );
};

export default Register;
