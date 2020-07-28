# NewsService
News Service to aggregate news from multiple sources and show them based on custom rules

# Endpoints:

# Getting news:
    GET     https://{host}
    GET     https://{host}/api/news
    GET     https://{host}?pageNumber=1&pageSize=4
    GET     https://{host}/api/news?pageNumber=1&pageSize=4
        
# News Source Registration:
    POST    https://{host}/api/newssource?sourceKey=InternalNews
    
# Adding news to a new source:
    PUT     https://{host}/api/newssource/1
    Body:
    [
        {
            "Heading": "P1",
            "Content": "priority news 1",
            "Category": "Political",
            "IsPriority": true
        },
        {
            "Heading": "P2",
            "Content": "priority news 2",
            "Category": "Political",
            "IsPriority": true
        },
        {
            "Heading": "N1",
            "Content": "normal news 1",
            "Category": "Sports",
            "IsPriority": false
        },
        {
            "Heading": "A1",
            "Content": "Ad 1",
            "Category": "Advertisements",
            "IsPriority": false
        }
    ]
    
# Unregister a news source:
    DEL     https://{host}/api/newssource?sourceKey=InternalNews
    
