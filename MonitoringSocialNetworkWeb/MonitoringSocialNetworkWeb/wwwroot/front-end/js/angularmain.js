function initAngular(version) {
    app = angular.module('MyApp', ['ui.router', 'ui.bootstrap']);
    //Config Routering
    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode(true);
        $urlRouterProvider.otherwise("/");
        $stateProvider
            .state('trang-chu', {
                url: "/",
                templateUrl: "template/pages/index.htm?" + version,
                controller: "System"
            })
            .state('gioi-thieu', {
                url: "/gioi-thieu",
                templateUrl: "template/pages/about.htm?" + version,
                controller: "System"
            })
            .state('giang-vien', {
                url: "/giang-vien",
                templateUrl: "template/pages/teacher.htm?" + version,
                controller: "System"
            })
            .state('chuong-trinh-hoc', {
                url: "/chuong-trinh-hoc",
                templateUrl: "template/pages/lesson.htm?" + version,
                controller: "System"
            })
            .state('hoc-phi', {
                url: "/hoc-phi",
                templateUrl: "template/pages/tuition.htm?" + version,
                controller: "System"
            })
            .state('bai-viet', {
                url: "/bai-viet",
                templateUrl: "template/pages/blog.htm?" + version,
                controller: "System"
            })
            .state('lien-he', {
                url: "/lien-he",
                templateUrl: "template/pages/contact.htm?" + version,
                controller: "System"
            })
            .state('doc', {
                url: "/bai-viet/:id",
                templateUrl: function ($stateParams) {
                    return 'Blog/GetContentBlog/' + $stateParams.id;
                },
                controller: "System"
            })
    });
    app.filter('sce', ['$sce', function ($sce) {
        return $sce.trustAsHtml;
    }])
    //Direction

    //Controller
    app.controller('System', function ($scope, $http, $location, $anchorScroll, $timeout) {

        //View
        $scope.indexView = 'template/pages/index.htm?' + version;
        $scope.aboutView = 'template/pages/about.htm?' + version;
        $scope.teacherView = 'template/pages/teacher.htm?' + version;
        $scope.lessonView = 'template/pages/lesson.htm?' + version;
        $scope.tuitionView = 'template/pages/tuition.htm?' + version;
        $scope.blogView = 'template/pages/blog.htm?' + version;
        $scope.contactView = 'template/pages/contact.htm?' + version;
        $scope.LessInfoView = 'template/LessInfo.htm?' + version;
        $scope.NavbarView = 'template/Navbar.htm?' + version;

        $scope.WelcomeView = 'template/Welcome.htm?' + version;
        $scope.AchrimentView = 'template/Achriment.htm?' + version;
        $scope.OpinionParentView = 'template/OpinionParent.htm?' + version;
        $scope.RequestView = 'template/Request.htm?' + version;
        $scope.FooterImageView = 'template/FooterImage.htm?' + version;
        $scope.BlogMainView = 'template/Blog.htm?' + version;
        $scope.TuitionMainView = 'template/Tuition.htm?' + version;
        $scope.TeacherMainView = 'template/Teacher.htm?' + version;
        $scope.LessonMainView = 'template/Lesson.htm?' + version;
        $scope.CompanyView = 'template/CompanyInfo.htm?' + version;
        $scope.ImageHeaderView = 'template/ImageHeader.htm?' + version;
        $scope.MapView = 'template/Map.htm?' + version;

        //Property
        //$scope.Teacher = [];
        //CallBack Repeat

        //Method
        $scope.ScrollUp = function () {
            $anchorScroll();
            $('.navbar-collapse').collapse('hide');
            $timeout(function () {
                reloadAnimate();
            });
        }
        $scope.isActive = function (viewLocation) {
            return viewLocation === $location.path();
        };
        $scope.GetConfiguration = function () {
            var option = {
                method: 'get',
                url: 'Configuration/GetConfiguration',
            }
            $http(option).then(function (response) {
                if (response.data !== null) {
                    $scope.System = response.data.model;
                    $scope.System.recentBlog = response.data.recentBlog;
                    $timeout(function () {
                        reloadAnimate();
                    });

                }
            });
        }
        $scope.GetFooterImage = function () {
            var option = {
                method: 'get',
                url: 'Configuration/GetListImage',
                params: {
                    typeImage: 1,
                    isShow: true,
                }
            }
            $http(option).then(function (response) {
                if (response.data !== null) {
                    $scope.FooterImage = response.data.data;
                    $timeout(function () {
                        reloadAnimate();
                        $('.image-popup').magnificPopup({
                            type: 'image',
                            closeOnContentClick: true,
                            closeBtnInside: false,
                            fixedContentPos: true,
                            mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
                            gallery: {
                                enabled: true,
                                navigateByImgClick: true,
                                preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
                            },
                            image: {
                                verticalFit: true
                            },
                            zoom: {
                                enabled: true,
                                duration: 300 // don't foget to change the duration also in CSS
                            }
                        });
                    });
                }
            });
        }
        $scope.GetLesson = function () {
            var option = {
                method: 'get',
                url: 'Lesson/GetListLesson',
            }
            $http(option).then(function (response) {
                if (response.data !== null) {
                    $scope.Lesson = response.data.data;
                    $timeout(function () {
                        reloadAnimate();
                    });
                }
            });
        }
        $scope.GetTeacher = function () {
            var option = {
                method: 'get',
                url: 'Teacher/GetListTeacher',
            }
            $http(option).then(function (response) {

                if (response.data !== null) {
                    $scope.Teacher = response.data.data;
                    $timeout(function () {
                        reloadAnimate();
                    });

                }
            });
        }
        $scope.GetTuition = function () {
            var option = {
                method: 'get',
                url: 'Tuition/GetListTuition',
            }
            $http(option).then(function (response) {
                if (response.data !== null) {
                    $scope.Tuition = response.data.data;
                    $timeout(function () {
                        reloadAnimate();
                    });
                }
            });
        }
        $scope.GetListBlogView = function () {
            var option = {
                method: 'get',
                url: 'Blog/GetListBlogView',
            }
            $http(option).then(function (response) {
                if (response.data !== null) {
                    $scope.Blog = response.data.data;
                    if ($scope.Blog != null) {
                        $scope.totalItems = $scope.Blog.length;
                        $scope.pageSize = 6,
                        $scope.currentPage = 1;
                    }
                    $timeout(function () {
                        reloadAnimate();
                    });
                }
            });
        }
    });

}