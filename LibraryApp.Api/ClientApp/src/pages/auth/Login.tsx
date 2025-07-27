const Login = () => {
  return (
    <form method="POST" className="flex flex-col">
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
        Login
      </button>
    </form>
  );
};

export default Login;
