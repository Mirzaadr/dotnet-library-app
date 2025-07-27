import { Button } from "@/components/ui/button";

const Profile = () => {
  return (
    <>
      {/* <!-- Profile Card Section --> */}
      <section className=" py-8">
        <div className="max-w-4xl mx-auto px-6">
          <div className="bg-accent rounded-2xl shadow-lg p-8 flex items-center space-x-6 relative overflow-hidden">
            {/* <!-- Decorative library card background --> */}
            <div className="absolute top-0 right-0 w-40 h-40 bg-primary opacity-10 rounded-full"></div>
            <div className="w-24 h-24 bg-neutral-200 rounded-full relative z-10"></div>
            <div className="space-y-2 z-10">
              <h2 className="text-3xl font-bold">[User Name]</h2>
              <p className="text-gray-600">Member Since: January 2025</p>
              <p className="text-gray-600">
                Library Card: <span className="font-medium">1234‑5678</span>
              </p>
            </div>
            <Button>Edit Profile</Button>
            {/* <button className="ml-auto z-10 text-white px-5 py-2 rounded-lg hover:bg-primary-dark transition">
              Edit Profile
            </button> */}
          </div>
        </div>
      </section>

      {/* <!-- Currently Borrowed as Cards --> */}
      <section className=" py-8">
        <div className="max-w-6xl mx-auto px-6">
          <h3 className="text-2xl font-semibold mb-6">Currently Borrowed</h3>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {/* <!-- Borrowed-book card --> */}
            <div className="bg-accent rounded-lg shadow-sm p-4 flex flex-col">
              <img
                src="https://booksofwonder.com/cdn/shop/products/9780062875174_e6a6a.jpg?v=1622131119"
                alt="Book cover"
                className="rounded-md mb-4 object-cover h-48 w-full"
              />
              <h4 className="font-semibold mb-1">Book Title One</h4>
              <p className="text-gray-600 text-sm mb-2">by Author One</p>
              <p className="text-gray-500 text-sm">
                Due: <span className="font-medium">Aug 10, 2025</span>
              </p>
              <div className="mt-auto pt-3">
                <Button className="w-full">Renew</Button>
              </div>
            </div>
            {/* <!-- Repeat similar cards for other current loans --> */}
          </div>
        </div>
      </section>

      {/* <!-- History Section --> */}
      <section className="py-8 flex-grow">
        <div className="max-w-6xl mx-auto px-6">
          <h3 className="text-2xl font-semibold mb-6">Borrowing History</h3>
          <div className="space-y-4">
            {/* <!-- Past-book card --> */}
            <div className="bg-accent rounded-lg shadow-sm p-4 flex items-center">
              <img
                src="https://booksofwonder.com/cdn/shop/products/9780062875174_e6a6a.jpg?v=1622131119"
                alt=""
                className="w-20 h-28 object-cover rounded-md mr-4"
              />
              <div className="flex-grow">
                <h4 className="font-semibold">Book Two</h4>
                <p className="text-gray-600 text-sm">by Author Two</p>
                <p className="text-gray-500 text-sm">
                  Checked Out: Jun 1, 2025
                </p>
                <p className="text-gray-500 text-sm">Returned: Jun 15, 2025</p>
              </div>
            </div>
            {/* <!-- Repeat history cards --> */}
          </div>
        </div>
      </section>
    </>
  );
};

export default Profile;
