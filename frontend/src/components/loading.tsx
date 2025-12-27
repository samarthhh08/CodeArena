const Loading = ({ text = "Loading..." }) => {
  return (
    <div className="flex flex-col items-center justify-center gap-3 py-8">
      {/* Spinner */}
      <div className="w-8 h-8 border-4 border-gray-300 border-t-blue-600 rounded-full animate-spin" />

      {/* Text */}
      <p className="text-sm text-gray-600">{text}</p>
    </div>
  );
};

export default Loading;
